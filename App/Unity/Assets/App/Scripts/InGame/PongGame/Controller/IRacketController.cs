namespace App.InGame
{
	public interface IRacketController
	{
		void Setup(Racket racket, Ball ball);
		void FixedUpdate();
	}
}