using System.Windows;

namespace AirlinePlanificator.Views.Utilities
{
    /// <summary>
    /// EditorTemplateRule.
    /// </summary>
    public class EditorTemplateRule
    {
        private string propertyName;
        private DataTemplate dataTemplate;

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public string PropertyName
        {
            get
            {
                return propertyName;
            }
            set
            {
                propertyName = value;
            }
        }

        /// <summary>
        /// Gets or sets the data template.
        /// </summary>
        /// <value>The data template.</value>
        public DataTemplate DataTemplate
        {
            get
            {
                return dataTemplate;
            }
            set
            {
                dataTemplate = value;
            }
        }
    }
}