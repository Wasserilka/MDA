using System.ComponentModel;

namespace MDA.Models
{
    public class Table
    {
        private State state;
        public int Id { get; }
        public State State 
        {
            get { return state; } 
            private set 
            {
                state = value;
                if (value == State.Booked)
                {
                    Task.Run(async () => 
                    {
                        await Task.Delay(60000);
                        state = State.Free;
                    });
                }
            } 
        }
        public int SeatsCount { get; }

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
    }
}
