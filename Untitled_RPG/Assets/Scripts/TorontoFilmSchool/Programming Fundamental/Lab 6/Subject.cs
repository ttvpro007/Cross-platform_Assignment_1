using System.Collections.Generic;

namespace ObserverPattern
{
    public class Subject
    {
        List<Observer> observers = new List<Observer>();

        public void Notify()
        {
            if (observers.Count > 0)
            {
                foreach (Observer observer in observers)
                {
                    observer.OnNotify();
                }
            }
        }

        public void Notify(bool check)
        {
            if (observers.Count > 0)
            {
                foreach (Observer observer in observers)
                {
                    observer.OnNotify(check);
                }
            }
        }

        public void AddObserver(Observer observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(Observer observer)
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }
    }
}
