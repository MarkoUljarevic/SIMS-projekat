using System.Collections.Generic;

namespace Hospital.View.DoctorView.Validation
{
    public class ValidationErrors : BindableBase
    {
        private readonly Dictionary<string, string> _validationErrors = new Dictionary<string, string>();

        public bool IsValid
        {
            get
            {
                return _validationErrors.Count < 1;
            }
        }

        public string this[string fieldName]
        {
            get
            {
                return _validationErrors.ContainsKey(fieldName) ?
                    _validationErrors[fieldName] : string.Empty;
            }



            set
            {
                if (_validationErrors.ContainsKey(fieldName))
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        _validationErrors.Remove(fieldName);
                    }
                    else
                    {
                        _validationErrors[fieldName] = value;
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        _validationErrors.Add(fieldName, value);
                    }
                }
                OnPropertyChanged("IsValid");
            }
        }

        public void Clear()
        {
            _validationErrors.Clear();
        }
    }
}