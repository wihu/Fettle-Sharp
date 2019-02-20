using System;
using Fettle.Builder;

namespace FettleSharp
{
	public class State : Fettle.Enumeration
	{
		public static State Initial = new State(0, "Initial");
		public static State One = new State(1, "One");
		public static State Two = new State(2, "Two");
		public static State Three = new State(3, "Three");

		public State(int id, string name) : base(id, name)
		{
		}
	}

    class MainClass
    {
        public static void Main(string[] args)
        {
            var builder = StateMachineBuilder<State, String, Fettle.Void>.Create();
			builder.Transition().From(State.Initial).To(State.One).On("hello");
			builder.Transition().From(State.One).To(State.Two).On("jump");

			var template = builder.BuildTransitionModel();
			var machine = template.NewStateMachineWithInitialState(State.Initial);
            machine.FireEvent("hello");

            Console.WriteLine("State = " + machine.CurrentState);

            machine.FireEvent("jump");

            Console.WriteLine("State = " + machine.CurrentState);
        }
    }
}
