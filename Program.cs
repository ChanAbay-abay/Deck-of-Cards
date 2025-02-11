using System;
using System.Collections.Generic;

class Program{
    static void Main(){
        Deck deck = null;
        while (true){
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Create Deck");
            Console.WriteLine("2. Shuffle Deck");
            Console.WriteLine("3. Deal Cards");
            Console.WriteLine("4. Display Deck");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();
            switch(choice){
                case "1":
                    deck = new Deck();
                    Console.WriteLine("Deck created successfully.");
                    break;
                case "2":
                    if(deck == null || deck.IsEmpty()){
                        Console.WriteLine("Deck is empty. Create a deck first.");
                    }else{
                        deck.Shuffle();
                        Console.WriteLine("Deck shuffled successfully.");
                    }
                    break;
                case "3":
                    if(deck == null || deck.IsEmpty())
                        Console.WriteLine("Deck is empty. Create a deck first.");
                    else{
                        Console.Write("Enter number of cards to deal: ");
                        if(int.TryParse(Console.ReadLine(), out int count)){
                            var dealtCards = deck.Deal(count);
                            if(dealtCards.Count > 0){
                                Console.WriteLine("Dealt Cards:");
                                foreach(var card in dealtCards)
                                    Console.WriteLine(card);
                            }
                        }else{
                            Console.WriteLine("Invalid number.");
                        }
                    }
                    break;
                case "4":
                    if(deck == null || deck.IsEmpty())
                        Console.WriteLine("Deck is empty. Create a deck first.");
                    else{
                        deck.Display();
                    }
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please select from the menu.");
                    break;
            }
        }
    }
}

class Card{
    public string Suit { get; }
    public string Rank { get; }

    public Card(string suit, string rank){
        Suit = suit;
        Rank = rank;
    }

    public override string ToString(){
        return $"Suit: {Suit}; Rank: {Rank}";
    }
}

class Deck{
    private Stack<Card> cards = new Stack<Card>();
    private static readonly string[] Suits = { "Cloves", "Diamond", "Heart", "Spade" };
    private static readonly string[] Ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Ace", "Jack", "Queen", "King" };

    public Deck(){
        List<Card> deckList = new List<Card>();
        foreach(var suit in Suits){
            foreach(var rank in Ranks){
                deckList.Add(new Card(suit, rank));
            }
        }
        cards = new Stack<Card>(deckList);
    }

    public void Shuffle(){
        Random rng = new Random();
        List<Card> shuffledList = new List<Card>(cards);
        int n = shuffledList.Count;
        while(n > 1){
            n--;
            int k = rng.Next(n + 1);
            (shuffledList[k], shuffledList[n]) = (shuffledList[n], shuffledList[k]);
        }
        cards = new Stack<Card>(shuffledList);
    }

    public List<Card> Deal(int count){
        List<Card> dealtCards = new List<Card>();
        for(int i = 0; i < count && cards.Count > 0; i++){
            dealtCards.Add(cards.Pop());
        }
        return dealtCards;
    }

    public void Display(){
        Console.WriteLine("\nDeck:");
        foreach(var card in cards){
            Console.WriteLine(card);
        }
        Console.WriteLine($"Total cards remaining: {cards.Count}");
    }

    public bool IsEmpty(){
        return cards.Count == 0;
    }
}
