namespace ObserverPattern
{
    public abstract class Observer
    {
        public abstract void OnNotify();
        public abstract void OnNotify(bool check);
    }
}
