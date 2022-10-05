using System.Collections;
using System.Reflection;
using static System.String;


namespace MainApp
{
    public class BindablePicker : Picker
    {
        readonly List<PickerItem> _pickerItems = new List<PickerItem>();
        bool _ignoreItemChangedEvents = false;

        public BindablePicker()
        {
            this.SelectedIndexChanged += OnSelectedIndexChanged;
        }

        public string DisplayPropertyName { get; set; }
        public string ValuePropertyName { get; set; }


        public static BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(BindablePicker), propertyChanged: OnItemsSourceChanged);
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        static void OnItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            BindablePicker @this = (BindablePicker)bindable;

            @this.Items.Clear();
            @this._pickerItems.Clear();

            @this.SelectedItem = null;
            @this.SelectedValue = null;

            if (newvalue == null)
                return;

            var newEnumerable = newvalue as IEnumerable;
            if (newEnumerable == null)
                throw new InvalidOperationException("ItemsSource must implement IEnumerable");

            var items = newEnumerable.AsQueryable().Cast<object>().ToArray();
            if (items.Any(item => item == null))
                throw new InvalidOperationException("One or more items in ItemsSource property are null");

            foreach (var i in items)
                @this._pickerItems.Add(@this.CreatePickerItem(i));

            foreach (var i in @this._pickerItems)
                @this.Items.Add(i.DisplayText);
        }


        public static BindableProperty SelectedItemProperty = BindableProperty.Create(
            propertyName: nameof(SelectedItem),
            returnType: typeof(object),
            declaringType: typeof(BindablePicker),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnSelectedItemChanged);

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            BindablePicker @this = (BindablePicker)bindable;
            if (@this._ignoreItemChangedEvents)
                return;

            if (newvalue == null)
            {
                @this.SelectedIndex = -1;
                return;
            }
            if (@this._pickerItems == null) //Note: _pickerItems is set whenever ItemsSource property is set
                throw new InvalidOperationException("Must set ItemsSource property before SelectedItem");

            if (@this._pickerItems.Count == 0)
                throw new InvalidOperationException("Cannot set SelectedItem property when no items are present");

            var newItemIndex = @this.FindItemIndex(pi => pi.Item.Equals(newvalue));
            if (newItemIndex < 0)
                throw new InvalidOperationException("New value for SelectedItem does not match any item in ItemsSource");

            @this._ignoreItemChangedEvents = true;
            try
            {
                @this.SelectedIndex = newItemIndex;
            }
            finally
            {
                @this._ignoreItemChangedEvents = false;
            }

            //Note: INotifyPropertyChanged handled by OnSelectedIndexChanged(...)
        }


        public static BindableProperty SelectedValueProperty = BindableProperty.Create(
            propertyName: nameof(SelectedValue),
            returnType: typeof(object),
            declaringType: typeof(BindablePicker),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnSelectedValueChanged);


        //public static BindableProperty SelectedValueProperty = BindableProperty.Create<BindablePicker, object>(o => o.SelectedVAlue)
        public object SelectedValue
        {
            get { return GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        static void OnSelectedValueChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            BindablePicker @this = (BindablePicker)bindable;
            if (@this._ignoreItemChangedEvents)
                return;

            if (newvalue == null)
            {
                @this.SelectedItem = -1;
                return;
            }

            if (@this._pickerItems == null) //Note: _pickerItems is set whenever ItemsSource SelectedValue is set
                throw new InvalidOperationException("Must set ItemsSource property before SelectedItem");

            if (@this._pickerItems.Count == 0)
                throw new InvalidOperationException("Cannot set SelectedValue property when no items are present");

            var newItemIndex = @this.FindItemIndex(pi => pi.Value.Equals(newvalue));
            if (newItemIndex < 0)
                throw new InvalidOperationException("New value for SelectedValue does not match any item in ItemsSource");

            @this._ignoreItemChangedEvents = true;
            try
            {
                @this.SelectedIndex = newItemIndex;
            }
            finally
            {
                @this._ignoreItemChangedEvents = false;
            }

            //Note: INotifyPropertyChanged handled by OnSelectedIndexChanged(...)
        }

        void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (_ignoreItemChangedEvents)
                return;

            _ignoreItemChangedEvents = true;
            try
            {
                SetValue(SelectedItemProperty, _pickerItems[this.SelectedIndex].Item);
                SetValue(SelectedValueProperty, _pickerItems[this.SelectedIndex].Value);
            }
            finally
            {
                _ignoreItemChangedEvents = false;
            }
        }

        int FindItemIndex(Func<PickerItem, bool> predicate)
        {
            for (var i = 0; i < _pickerItems.Count; ++i)
                if (predicate(_pickerItems[i]))
                    return i;
            return -1;
        }

        PickerItem CreatePickerItem(object item)
        {
            if (item == null)
                return new PickerItem(null, null, "Error: null item in ItemsSource");

            object value = item;
            if (!IsNullOrWhiteSpace(this.ValuePropertyName))
            {
                if (!TryGetPropertyValue(item, this.ValuePropertyName, out value))
                    return new PickerItem(item, item, $"Error: could not read ValueProperty '{this.ValuePropertyName}'");
            }

            string displayText;
            if (!IsNullOrWhiteSpace(this.DisplayPropertyName))
            {
                object displayValue;
                if (!TryGetPropertyValue(item, this.DisplayPropertyName, out displayValue))
                    return new PickerItem(item, value, $"Error: could not read DisplayProperty '{this.DisplayPropertyName}'");

                displayText = displayValue.ToString();
            }
            else
                displayText = value?.ToString() ?? "<null>";

            return new PickerItem(item, value, displayText);
        }

      

        static bool TryGetPropertyValue(object item, string propertyName, out object value)
        {
            value = null;

            var declaredProperty = item.GetType().GetTypeInfo().GetDeclaredProperty(propertyName);
            if (declaredProperty == null)
                return false;

            var getMethod = declaredProperty.GetMethod;
            if (getMethod == null)
                return false;

            value = getMethod.Invoke(item, new object[] { });
            return true;
        }

        class PickerItem
        {
            public object Item { get; }
            public string DisplayText { get; }
            public object Value { get; }

            public PickerItem(object item, object value, string displayText)
            {
                this.Item = item;
                this.DisplayText = displayText;
                this.Value = value;
            }
        }
    }
}
