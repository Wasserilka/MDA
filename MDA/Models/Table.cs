using Messaging;
using System.ComponentModel;

namespace Booking.Models
{
    public class Table
    {
        private State _state;
        private Dish? _dish;
        private Guid _clientId;

        public int Id { get; }
        public State State 
        {
            get { return _state; } 
            private set 
            {
                _state = value;
                if (value == State.Booked)
                {
                    Task.Run(async () => 
                    {
                        await Task.Delay(300000);
                        _state = State.Free;
                    });
                }
                else if (value == State.Free)
                {
                    _dish = null;
                }
            } 
        }
        public Dish? Dish { get { return _dish; } }
        public int SeatsCount { get; }
        public Guid ClientId { get { return _clientId; } }

        public Table(int id)
        {
            Id = id;
            State = State.Free;
            SeatsCount =  new Random().Next(2, 5);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool SetState(State state)
        {
            if (state == State)
            {
                return false;
            }

            State = state;
            return true;
        }

        public void SetOrder(Dish? dish, Guid clientId) 
        { 
            _dish = dish;
            _clientId = clientId;
        } 
    }
}
