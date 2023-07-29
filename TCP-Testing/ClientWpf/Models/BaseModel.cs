using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClientWpf.Models
{
    /// <summary>
    /// Класс, где реализован интерфейс по автообновлению данных между частями view и model
    /// </summary>
    internal class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
