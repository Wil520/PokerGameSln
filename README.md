## PokerGameSln
A poker game library (in C#) which evaluates who are the winner(s) among several 5 card poker hands and the unit tests.
The following poker hands are implemented:
---------------------------
• Flush
• Three of a Kind
• One Pair
• High Card
## How to run
- cd PokerGameSln
- dotnet build
- dotnet test
## Some ideas behind the code
1. In order to make the code easy to extend, the IRankingStrategy interface is created. If we want to use a different strategy to get winners, we only need to switch to a new implementation of IRankingStrategy, there is no need to change the code of the existing strategy.

2. Still the extensibility. The requirement is to implement four types of poker hands, but considering the possibility of adding new types, to facilitate the extensions, the ScoreCalculator is designed, and the score of a certain hand is calculated by a specific subclass. If we want to add a new hand, we only need to add a new implementation of ScoreCalculator. There is no need to modify the existing ScoreCalculator of other types, so it won't have any impact on the score calculation of the existing hands.

3. To rank the hands in a simple way instead of comparing the cards of different players one by one, a score-based strategy is designed. In this way, the players can get their scores from their specific ScoreCalculators, and then they can be ranked in the same way in the BasicScoreRankingStrategy no matter what types of cards they have, making it simple and easy to understand.

4. In order to make the code concise, LINQ and lambda expressions are used instead of lots of loops.

5. Test coverage. Players with different types of hands and the same type of hand are covered in the test code. For better test coverage, the HandCreator is created, which can randomly generate different types of cards, and then the randomly generated cards are used as input in the tests to ensure the correctness of the ranking algorithm.

6. Negative cases. Various possible negative cases are checked in the code and tested in the test cases. For example, whether the strategy and players are empty, whether the number of cards is correct, the validity of the suit and rank passed in, whether there are duplicate cards, and whether a specific hand type has not been implemented.
