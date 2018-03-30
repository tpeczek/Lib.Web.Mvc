namespace Lib.Web.Mvc.JQuery.JqGrid
{
    /// <summary>
    /// Represent string with state setted or not setted
    /// </summary>
    public class SettedString
    {
        private string _value;
        /// <summary>
        /// Is string was setted
        /// </summary>
        public bool IsSetted { get; }
        /// <summary>
        /// Return value. If string is null or empty it returns string "null"
        /// </summary>
        public string Value
        {
            get => string.IsNullOrWhiteSpace(_value) ? "null" : _value;
            set => _value = value;
        }

        /// <summary>
        /// Initialize new instance of SettedString
        /// </summary>
        /// <param name="isSetted">Is string setted</param>
        /// <param name="value">String value</param>
        public SettedString(bool isSetted, string value)
        {
            IsSetted = isSetted;
            Value = value;
        }

        /// <summary>
        /// Return Value
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }
    }
}