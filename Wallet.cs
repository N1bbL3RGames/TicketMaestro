//LAUREN NGUYENPHU

public class Wallet {
        
        public int funds;
        private string card;
        private int securityCode;
        private string expiration;
        private int balance;

        private ArrayList cardNumbers = new ArrayList();

        public Wallet(int funds, int securityCode, int balance, string card, string expiration) {

            funds = this.funds;
            securityCode = this.securityCode;
            balance = this.balance;
            card = this.card;
            expiration = this.expiration;
            cardNumbers.Add(card);            
        }

        public void addFunds(int funds, int balance) {
            balance = funds + balance;
        }

        private void addCard(string card) {
            cardNumbers.Add(card);
        }

        private void removeCard(int cardPosition) {
            cardNumbers.Remove(cardPosition - 1); // ArrayList start at 0
        }
}