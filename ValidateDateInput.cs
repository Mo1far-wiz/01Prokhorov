using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01Prokhorov
{
    public class ValidateDateInput
    {
        public string ErrorMsg { get; }
        public ValidateDateInput(string errorMsg) { ErrorMsg = errorMsg; }
    }
    public delegate void ValidateDateInputHandler(object? sender, ValidateDateInput e);

    public interface INotifyDataInputInvalid
    {
        event ValidateDateInputHandler? DateInputInvalid;
    }
}
