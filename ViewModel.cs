using _01Prokhorov;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

sealed class ViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public event ValidateDateInputHandler? ValidateDateInput;
    private Model _model;

    public ViewModel(ValidateDateInputHandler? errorHandler)
    {
        _model = new Model();
        ValidateDateInput += errorHandler;
    }

    private void OnPropertyChange(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public DateTime BirthDate
    {
        get { return _model.BirthDate; }
        set
        {
            if (_model.BirthDate != value)
            {
                _model.BirthDate = value;
                OnPropertyChange("BirthDate");
                OnPropertyChange("Age");
                OnPropertyChange("AsianSing");
                OnPropertyChange("WesternSing");
                OnPropertyChange("AsianSignString");
                OnPropertyChange("WesternSignString");
            }
        }
    }

    private void OnDataInputValidate(string errorMsg)
    {
        if (ValidateDateInput != null)
        {
            ValidateDateInput(this, new ValidateDateInput(errorMsg));
        }
    }

    private string GetAsianSigns(DateTime? birthDate)
    {
        if(birthDate == null)
            return string.Empty;

        int chineseHoroscopeIndex = (birthDate.Value.Year - 4) % 12;

        switch (chineseHoroscopeIndex)
        {
            case 0:
                return "Rat";
            case 1:
                return "Ox";
            case 2:
                return "Tiger";
            case 3:
                return "Rabbit";
            case 4:
                return "Dragon";
            case 5:
                return "Snake";
            case 6:
                return "Horse";
            case 7:
                return "Goat";
            case 8:
                return "Monkey";
            case 9:
                return "Rooster";
            case 10:
                return "Dog";
            case 11:
                return "Pig";
            default:
                return "Impossible date";
        }

    }
    private string GetWesternSigns(DateTime? birthDate)
    {
        if(birthDate == null)
            return string.Empty;

        int month = birthDate.Value.Month;
        int day = birthDate.Value.Day;

        if ((month == 3 && day >= 21) || (month == 4 && day <= 19))
        {
            return "ARIES";
        }
        else if ((month == 4 && day >= 20) || (month == 5 && day <= 20))
        {
            return "TAURUS";
        }
        else if ((month == 5 && day >= 21) || (month == 6 && day <= 20))
        {
            return "GEMINI";
        }
        else if ((month == 6 && day >= 21) || (month == 7 && day <= 22))
        {
            return "CANCER";
        }
        else if ((month == 7 && day >= 23) || (month == 8 && day <= 22))
        {
            return "LEO";
        }
        else if ((month == 8 && day >= 23) || (month == 9 && day <= 22))
        {
            return "VIRGO";
        }
        else if ((month == 9 && day >= 23) || (month == 10 && day <= 22))
        {
            return "LIBRA";
        }
        else if ((month == 10 && day >= 23) || (month == 11 && day <= 21))
        {
            return "SCORPIUS";
        }
        else if ((month == 11 && day >= 22) || (month == 12 && day <= 21))
        {
            return "SAGITTARIUS";
        }
        else if ((month == 12 && day >= 22) || (month == 1 && day <= 19))
        {
            return "CAPRICORN";
        }
        else if ((month == 1 && day >= 20) || (month == 2 && day <= 18))
        {
            return "AQUARIUS";
        }
        else if ((month == 2 && day >= 19) || (month == 3 && day <= 20))
        {
            return "PISCES";
        }
        else
        {
            return "Impossible date";
        }

    }

    public string WesternSignString
    {
        get
        {
            return GetWesternSigns(BirthDate);
        }
    }

    public string AsianSignString
    {
        get
        {
            return GetAsianSigns(BirthDate);
        }
    }

    public int Age
    {
        get
        {
            DateTime currDay = DateTime.Today;
            int age = currDay.Year - _model.BirthDate.Year;
            if (age < 0 || age > 135)
            {
                OnDataInputValidate("Impossible age!");
                age = age < 0 ? 0 : 135;
            }
            if (_model.BirthDate.Month == currDay.Month && _model.BirthDate.Day == currDay.Day)
            {
                OnDataInputValidate("Happy birthday!");
            }

            return age;
        }
    }
}