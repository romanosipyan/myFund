using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace myFund
{
    public class AutofacDataProvider : DependencyObject
    {
        public static readonly DependencyProperty AutofacContainerProperty =
            DependencyProperty.Register("AutofacContainer",
                typeof(IContainer),
                typeof(AutofacDataProvider),
                new PropertyMetadata(null, AutofacValuesChanged));

        public IContainer AutofacContainer
        {
            get { return (IContainer)GetValue(AutofacContainerProperty); }
            set { SetValue(AutofacContainerProperty, value); }
        }

        public static readonly DependencyProperty DataTypeNameProperty =
            DependencyProperty.Register("DataTypeName",
            typeof(string),
            typeof(AutofacDataProvider),
            new PropertyMetadata(null, AutofacValuesChanged));

        public string DataTypeName
        {
            get { return (string)GetValue(DataTypeNameProperty); }
            set { SetValue(DataTypeNameProperty, value); }
        }

        private static readonly DependencyPropertyKey DataPropertyKey =
            DependencyProperty.RegisterReadOnly("Data",
            typeof(object),
            typeof(AutofacDataProvider),
            new PropertyMetadata(null));

        public static readonly DependencyProperty DataProperty =
            DataPropertyKey.DependencyProperty;

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            private set { SetValue(DataProperty, value); }
        }

        private static void AutofacValuesChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var container = d.GetValue(AutofacContainerProperty) as IContainer;
            var typeName = d.GetValue(DataTypeNameProperty) as string;
            if (container == null || string.IsNullOrEmpty(typeName))
                return;
            var assembly = Assembly.GetExecutingAssembly();
            var type = assembly.GetType(typeName, false, true);
            if (type == null)
                return;
            var data = container.Resolve(type);
            d.SetValue(DataPropertyKey, data);
        }
    }
}
