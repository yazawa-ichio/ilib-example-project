namespace App.InGame
{
	public interface IPongGameController
	{
		event System.Action<object> SendTarget;
		event System.Action<object> Broadcast;
		bool IsHost { get; }
		Role Role { get; }
		void FixedUpdate();
		void Setup(Ball ball, Racket racket1, Racket racket2);
		void OnReceive(object obj);
	}
}