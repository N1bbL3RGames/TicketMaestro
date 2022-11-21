using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Terra
{
    internal class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //Backgrounds
        public Texture2D blank;

        //Non-Button Icons
        public Texture2D bell;
        public Texture2D gear;
        public Texture2D circle;
        public Texture2D search;
        public Texture2D visa;
        public Texture2D mastercard;
        public Texture2D discover;
        public Texture2D americanExpress;
        public Texture2D paypal;
        public Texture2D plus;
        public Texture2D minus;
        public Texture2D logo;

        //Button Icons
        public Texture2D home;
        public Texture2D plan;
        public Texture2D cart;
        public Texture2D profile;

        //User Input
        public KeyboardState kb;
        public KeyboardState oldKb;
        public MouseState ms;
        public MouseState oldMs;
        public Rectangle mouseRect;

        //Fonts
        public SpriteFont header;
        public SpriteFont prompt;
        public SpriteFont small;

        //User Interface
        public List<Button> buttons;
        public List<Image> images;
        public List<Output> outputs;
        public List<TextBox> inputs;
        public List<TextButton> textButtons;
        public List<ImageButton> imageButtons;
        public List<CheckBox> checks;
        public List<TicketList> tickets;
        public List<Slider> sliders;
        public List<Container> containers;
        public List<Rectangle> themeBoxes;
        public List<Color> themePrimaries;
        public List<Color> themeSecondaries;

        //App Variables
        private List<string> typedInput;
        public AppState state;
        public AppState oldState;
        public bool enableButtons;
        public bool enableInput;
        public bool enableSlider;
        public int inputID;
        public int sliderID;
        public char toOutput;
        public AppUser user;
        public Color primaryTheme;
        public Color secondaryTheme;
        public int activeButton;
        public double cartValue;
        public double cartTax;
        public double cartTotal;
        public double balance;
        public string savedCard;
        public string balanceFloat;

        public Game1() : base()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";

            this.IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 360;
            graphics.PreferredBackBufferHeight = 740;
            graphics.ApplyChanges();

            oldKb = Keyboard.GetState();
            oldMs = Mouse.GetState();

            mouseRect = new Rectangle(oldMs.X, oldMs.Y, 1, 1);

            state = AppState.Login;
            oldState = state;
            enableButtons = false;
            enableInput = false;
            enableSlider = false;
            inputID = -1;
            sliderID = -1;
            toOutput = new char();
            primaryTheme = Color.DarkGreen;
            secondaryTheme = Color.LightGreen;
            activeButton = -1;
            cartValue = 0;
            cartTax = (double)((int)((cartValue * 0.0825) * 100)) / 100;
            cartTotal = cartValue + cartTax;
            balance = 20;
            balanceFloat = (balance * 100).ToString();
            balanceFloat = balanceFloat.Insert(balanceFloat.Length - 2, ".");
            
            savedCard = "1237-9548-8923-7281";

            user = new AppUser("Firstname Lastname", "fml100000@utdallas.edu", "123-456-7890", "1234 Main St", "Password");
            //user.email = "";
            //user.setPassword("");

            buttons = new List<Button>();
            images = new List<Image>();
            outputs = new List<Output>();
            inputs = new List<TextBox>();
            textButtons = new List<TextButton>();
            imageButtons = new List<ImageButton>();
            checks = new List<CheckBox>();
            tickets = new List<TicketList>();
            sliders = new List<Slider>();
            containers = new List<Container>();
            themeBoxes = new List<Rectangle>();
            themePrimaries = new List<Color>();
            themeSecondaries = new List<Color>();

            typedInput = new List<string>();

            //Fonts for Output
            header = this.Content.Load<SpriteFont>("font"); //Header Style
            prompt = this.Content.Load<SpriteFont>("prompt"); //Prompt Style
            small = this.Content.Load<SpriteFont>("small"); //Small Style

            //Icon Image Textures
            blank = this.Content.Load<Texture2D>("blank");
            bell = this.Content.Load<Texture2D>("bell");
            gear = this.Content.Load<Texture2D>("gear");
            circle = this.Content.Load<Texture2D>("circle");
            search = this.Content.Load<Texture2D>("search");
            visa = this.Content.Load<Texture2D>("visa");
            mastercard = this.Content.Load<Texture2D>("mastercard");
            discover = this.Content.Load<Texture2D>("discover");
            americanExpress = this.Content.Load<Texture2D>("americanexpress");
            paypal = this.Content.Load<Texture2D>("paypal");
            plus = this.Content.Load<Texture2D>("plus");
            minus = this.Content.Load<Texture2D>("minus");
            logo = this.Content.Load<Texture2D>("logo");

            //Button Image Textures
            home = this.Content.Load<Texture2D>("home");
            plan = this.Content.Load<Texture2D>("plan");
            cart = this.Content.Load<Texture2D>("cart");
            profile = this.Content.Load<Texture2D>("profile");

            //Any AppState UI
            buttons.Add(new Button(
                new Image(blank, new Rectangle(1, 651, 88, 88)), new Image(home, new Rectangle(30, 666, 30, 30)),
                new Output(small, new Vector2(28, 706), "Home", Color.Black), AppState.Any)); //0
            buttons.Add(new Button(
                new Image(blank, new Rectangle(91, 651, 88, 88)), new Image(plan, new Rectangle(110, 656, 50, 50)),
                new Output(small, new Vector2(122, 706), "Plan", Color.Black), AppState.Any)); //1
            buttons.Add(new Button(
                new Image(blank, new Rectangle(181, 651, 88, 88)), new Image(cart, new Rectangle(210, 666, 30, 30)),
                new Output(small, new Vector2(214, 706), "Cart", Color.Black), AppState.Any)); //2
            buttons.Add(new Button(
                new Image(blank, new Rectangle(271, 651, 88, 88)), new Image(profile, new Rectangle(300, 666, 30, 30)),
                new Output(small, new Vector2(298, 706), "Profile", Color.Black), AppState.Any)); //3
            imageButtons.Add(new ImageButton(new Image(bell, new Rectangle(270, 15, 30, 30)), AppState.Any)); //0
            imageButtons.Add(new ImageButton(new Image(gear, new Rectangle(315, 15, 30, 30)), AppState.Any)); //1
            outputs.Add(new Output(header, new Vector2(35, 20), "Ticket Maestro", Color.White, AppState.Any)); //0

            //Login AppState UI
            outputs.Add(new Output(prompt, new Vector2(35, 100), "Sign In", Color.Black, AppState.Login)); //1
            outputs.Add(new Output(prompt, new Vector2(35, 470), "Forgot Password?", Color.Black, AppState.Login)); //2
            outputs.Add(new Output(prompt, new Vector2(35, 555), "No Account?", Color.Black, AppState.Login)); //3
            outputs.Add(new Output(small, new Vector2(230, 240), "Remember Me", Color.Black, AppState.Login)); //4
            outputs.Add(new Output(small, new Vector2(65, 330), "Incorrect email or password, try again.", Color.White, AppState.Login)); //5
            inputs.Add(new TextBox(blank, new Rectangle(35, 130, 290, 40),
                new Output(small, new Vector2(40, 143), "Email: ", Color.Black), user.email, AppState.Login)); //0
            inputs.Add(new TextBox(blank, new Rectangle(35, 180, 290, 40),
                new Output(small, new Vector2(40, 193), "Password: ", Color.Black), user.getPassword(), AppState.Login)); //1
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(35, 495, 290, 40)),
                new Output(small, new Vector2(130, 508), "Password Reset", Color.Red), AppState.Login)); //0
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(35, 580, 290, 40)),
                new Output(small, new Vector2(154, 593), "Sign Up", Color.Red), AppState.Login)); //1
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(205, 270, 120, 40)),
                new Output(small, new Vector2(250, 282), "Login", Color.Black), AppState.Login)); //2
            checks.Add(new CheckBox(blank, new Rectangle(205, 238, 20, 20), Color.Black, false, AppState.Login)); //0

            //Profile AppState UI
            outputs.Add(new Output(prompt, new Vector2(35, 100), "Name", Color.Black, AppState.Profile)); //6
            outputs.Add(new Output(prompt, new Vector2(35, 220), "Phone Number", Color.Black, AppState.Profile)); //7
            outputs.Add(new Output(prompt, new Vector2(35, 340), "Email Address", Color.Black, AppState.Profile)); //8
            outputs.Add(new Output(prompt, new Vector2(35, 460), "Address", Color.Black, AppState.Profile)); //9
            inputs.Add(new TextBox(blank, new Rectangle(35, 130, 290, 40),
                new Output(small, new Vector2(40, 145), user.name, Color.Black), user.email, AppState.Profile)); //2
            inputs.Add(new TextBox(blank, new Rectangle(35, 250, 290, 40),
                new Output(small, new Vector2(40, 265), user.phoneNumber, Color.Black), user.email, AppState.Profile)); //3
            inputs.Add(new TextBox(blank, new Rectangle(35, 370, 290, 40),
                new Output(small, new Vector2(40, 385), user.email, Color.Black), user.email, AppState.Profile)); //4
            inputs.Add(new TextBox(blank, new Rectangle(35, 490, 290, 40),
                new Output(small, new Vector2(40, 505), user.address, Color.Black), user.email, AppState.Profile)); //5

            //Settings AppState UI
            images.Add(new Image(profile, new Rectangle(130, 85, 100, 100), AppState.Settings)); //0
            outputs.Add(new Output(prompt, new Vector2(128, 190), "Edit Settings", Color.Black, AppState.Settings)); //10
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(55, 245, 250, 40)),
                new Output(small, new Vector2(160, 258), "Logout", Color.Black), AppState.Settings)); //3
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(55, 300, 250, 40)),
                new Output(small, new Vector2(138, 313), "Change Theme", Color.Black), AppState.Settings)); //4
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(55, 355, 250, 40)),
                new Output(small, new Vector2(125, 368), "Toggle Notifications", Color.Red), AppState.Settings)); //5
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(55, 410, 250, 40)),
                new Output(small, new Vector2(107, 423), "Manage Payment Options", Color.Black), AppState.Settings)); //6
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(55, 465, 250, 40)),
                new Output(small, new Vector2(128, 478), "Change Language", Color.Red), AppState.Settings)); //7
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(55, 520, 250, 40)),
                new Output(small, new Vector2(160, 533), "About", Color.Red), AppState.Settings)); //8

            //Cart AppState UI
            outputs.Add(new Output(prompt, new Vector2(35, 80), "Purchased Tickets", Color.Black, AppState.Cart)); //11
            tickets.Add(new TicketList(new Rectangle(35, 110, 290, 400), prompt, AppState.Cart)); //0
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(35, 530, 290, 40)), 
                new Output(small, new Vector2(110, 543), "Select Payment Method", Color.Black), AppState.Cart)); //9
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(35, 590, 290, 40)),
                new Output(small, new Vector2(130, 603), "Purchase Tickets", Color.Black), AppState.Cart)); //10

            //Plan AppState UI
            inputs.Add(new TextBox(blank, new Rectangle(35, 80, 290, 40),
                new Output(small, new Vector2(40, 95), "Search: ", Color.Black), "", AppState.Plan)); //6
            images.Add(new Image(search, new Rectangle(290, 90, 20, 20), AppState.Plan)); //1
            sliders.Add(new Slider(new Output(small, new Vector2(35, 135), "Cost", Color.Black),
                "USD", 50, small, AppState.Plan)); //0
            sliders.Add(new Slider(new Output(small, new Vector2(35, 165), "Time", Color.Black),
                "hr", 5, small, AppState.Plan)); //1
            sliders.Add(new Slider(new Output(small, new Vector2(35, 195), "Distance", Color.Black),
                "mi", 100, small, AppState.Plan)); //2
            tickets.Add(new TicketList(new Rectangle(35, 250, 290, 380), prompt, AppState.Plan)); //1
            //containers.Add(new Container(new Rectangle(35, 460, 290, 170), AppState.Plan)); //0

            //Subsection of Plan AppState UI - Individual Tickets
            tickets[1].tickets.Add(new Ticket("Samuel A. Rail Transportation\n$24, 53 mi, 2.5 hrs", 24, 53, 2.5, plus));
            tickets[1].tickets.Add(new Ticket("Joe and Sons' Trams\n$15, 8 mi, 0.3 hrs", 15, 8, 0.3, plus));
            tickets[1].tickets.Add(new Ticket("Bob's Passenger Lines\n$12, 20 mi, 1 hr", 12, 20, 1, plus));
            tickets[1].tickets.Add(new Ticket("Carl Hanratty Railways\n$19, 30 mi, 1.5 hrs", 19, 30, 1.5, plus));

            //Payment Input AppState UI
            outputs.Add(new Output(small, new Vector2(111, 120), "Select Payment Method", Color.Black, AppState.PayInput)); //12
            outputs.Add(new Output(small, new Vector2(135, 170), "Account Credit", Color.Black, AppState.PayInput)); //13
            outputs.Add(new Output(small, new Vector2(118, 193), "Current Balance: $" + balanceFloat, Color.Black, AppState.PayInput)); //14
            outputs.Add(new Output(small, new Vector2(135, 263), "PayPal Transfer", Color.Black, AppState.PayInput)); //15
            outputs.Add(new Output(small, new Vector2(135, 330), "Card Ending In:", Color.Black, AppState.PayInput)); //16
            outputs.Add(new Output(small, new Vector2(165, 353), savedCard.Substring(15), Color.Black, AppState.PayInput)); //17
            outputs.Add(new Output(small, new Vector2(80, 520), "Please select a payment method.", Color.White, AppState.PayInput)); //18
            images.Add(new Image(paypal, new Rectangle(65, 250, 40, 40), AppState.PayInput)); //2
            images.Add(new Image(mastercard, new Rectangle(65, 330, 40, 40), AppState.PayInput)); //3
            containers.Add(new Container(new Rectangle(35, 160, 290, 60), AppState.PayInput)); //1
            containers.Add(new Container(new Rectangle(35, 240, 290, 60), AppState.PayInput)); //2
            containers.Add(new Container(new Rectangle(35, 320, 290, 60), AppState.PayInput)); //3
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(35, 460, 290, 40)),
                new Output(small, new Vector2(155, 473), "Confirm", Color.Black), AppState.PayInput)); //11
            checks.Add(new CheckBox(blank, new Rectangle(280, 180, 20, 20), Color.Black, false, AppState.PayInput)); //1
            checks.Add(new CheckBox(blank, new Rectangle(280, 260, 20, 20), Color.Black, false, AppState.PayInput)); //2
            checks.Add(new CheckBox(blank, new Rectangle(280, 340, 20, 20), Color.Black, false, AppState.PayInput)); //3

            //Payment Confirmation AppState UI
            outputs.Add(new Output(small, new Vector2(131, 120), "Confirm Amount", Color.Black, AppState.PayConfirm)); //19
            outputs.Add(new Output(small, new Vector2(80, 195), "Subtotal", Color.Black, AppState.PayConfirm)); //20
            outputs.Add(new Output(small, new Vector2(80, 245), "Tax", Color.Black, AppState.PayConfirm)); //21
            outputs.Add(new Output(small, new Vector2(80, 342), "Total", Color.Black, AppState.PayConfirm)); //22
            outputs.Add(new Output(small, new Vector2(260, 195), "$" + cartValue, Color.Black, AppState.PayConfirm)); //23
            outputs.Add(new Output(small, new Vector2(260, 245), "$" + cartTax, Color.Black, AppState.PayConfirm)); //24
            outputs.Add(new Output(small, new Vector2(260, 342), "$" + cartTotal, Color.Black, AppState.PayConfirm)); //25
            outputs.Add(new Output(small, new Vector2(65, 450), "Balance too low, please add more funds.", Color.White, AppState.PayConfirm)); //26
            containers.Add(new Container(new Rectangle(35, 160, 290, 140), AppState.PayConfirm)); //4
            containers.Add(new Container(new Rectangle(35, 320, 290, 60), AppState.PayConfirm)); //5
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(35, 400, 290, 40)),
                new Output(small, new Vector2(155, 413), "Confirm", Color.Black), AppState.PayConfirm)); //12

            //Payment Receipt AppState UI
            outputs.Add(new Output(small, new Vector2(125, 90), "Payment Confirmed", Color.Black, AppState.Receipt)); //27
            tickets.Add(new TicketList(new Rectangle(35, 130, 290, 330), prompt, AppState.Receipt)); //2

            //Home AppState UI
            outputs.Add(new Output(prompt, new Vector2(35, 185), "Active Tickets", Color.Black, AppState.Home)); //28
            tickets.Add(new TicketList(new Rectangle(35, 215, 290, 350), prompt, AppState.Home)); //3
            images.Add(new Image(logo, new Rectangle(90, 70, 180, 100), AppState.Home)); //4
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(35, 585, 290, 40)),
                new Output(small, new Vector2(155, 598), "Support", Color.Black), AppState.Home)); //13

            //Support AppState UI
            outputs.Add(new Output(prompt, new Vector2(103, 190), "Help and Support", Color.Black, AppState.Support)); //29
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(55, 245, 250, 40)),
                new Output(small, new Vector2(158, 258), "Call Us", Color.Red), AppState.Support)); //14
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(55, 300, 250, 40)),
                new Output(small, new Vector2(120, 313), "Email Us Feedback", Color.Red), AppState.Support)); //15
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(55, 355, 250, 40)),
                new Output(small, new Vector2(160, 368), "FAQs", Color.Red), AppState.Support)); //16
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(55, 410, 250, 40)),
                new Output(small, new Vector2(118, 423), "Terms & Conditions", Color.Red), AppState.Support)); //17

            //Add Funds AppState UI
            outputs.Add(new Output(prompt, new Vector2(83, 90), "Manage Payment Options", Color.Black, AppState.AddFunds)); //30
            outputs.Add(new Output(small, new Vector2(35, 390), "Current Card on File: " + savedCard, Color.Black, AppState.AddFunds)); //31
            outputs.Add(new Output(small, new Vector2(35, 420), "Current Wallet Balance: $" + balanceFloat, Color.Black, AppState.AddFunds)); //32
            containers.Add(new Container(new Rectangle(35, 170, 290, 180), AppState.AddFunds)); //6
            images.Add(new Image(visa, new Rectangle(60, 190, 60, 60), AppState.AddFunds)); //5
            images.Add(new Image(mastercard, new Rectangle(150, 190, 60, 60), AppState.AddFunds)); //6
            images.Add(new Image(discover, new Rectangle(240, 190, 60, 60), AppState.AddFunds)); //7
            images.Add(new Image(americanExpress, new Rectangle(105, 270, 60, 60), AppState.AddFunds)); //8
            images.Add(new Image(paypal, new Rectangle(195, 270, 60, 60), AppState.AddFunds)); //9
            inputs.Add(new TextBox(blank, new Rectangle(35, 450, 290, 40),
                new Output(small, new Vector2(40, 465), "$", Color.Black), "", AppState.AddFunds)); //7
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(35, 510, 145, 40)),
                new Output(small, new Vector2(55, 523), "Add This Amount", Color.Black), AppState.AddFunds)); //18

            //Change UI AppState UI
            themeBoxes.Add(new Rectangle(35, 120, 80, 80));
            themeBoxes.Add(new Rectangle(140, 120, 80, 80));
            themeBoxes.Add(new Rectangle(245, 120, 80, 80));
            themeBoxes.Add(new Rectangle(35, 225, 80, 80));
            themeBoxes.Add(new Rectangle(140, 225, 80, 80));
            themeBoxes.Add(new Rectangle(245, 225, 80, 80));
            themeBoxes.Add(new Rectangle(35, 330, 80, 80));
            themePrimaries.Add(Color.DarkRed);
            themePrimaries.Add(Color.DarkOrange);
            themePrimaries.Add(Color.DarkMagenta);
            themePrimaries.Add(Color.DarkGreen);
            themePrimaries.Add(Color.DarkBlue);
            themePrimaries.Add(Color.DarkViolet);
            themePrimaries.Add(Color.Black);
            themeSecondaries.Add(Color.LightPink);
            themeSecondaries.Add(Color.LightYellow);
            themeSecondaries.Add(new Color(139, 25, 139, 150));
            themeSecondaries.Add(Color.LightGreen);
            themeSecondaries.Add(Color.LightBlue);
            themeSecondaries.Add(new Color(148, 50, 211, 150));
            themeSecondaries.Add(Color.LightGray);

            //Default Input Values
            typedInput.Add("");
            typedInput.Add("");
            typedInput.Add(user.name);
            typedInput.Add(user.phoneNumber);
            typedInput.Add(user.email);
            typedInput.Add(user.address);
            typedInput.Add("");
            typedInput.Add("");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(this.GraphicsDevice);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            kb = Keyboard.GetState();
            ms = Mouse.GetState();
            mouseRect = new Rectangle(ms.X, ms.Y, 1, 1);

            for (int a = 0; a < buttons.Count; a++)
                if (ms.LeftButton == ButtonState.Pressed && oldMs.LeftButton != ButtonState.Pressed && mouseRect.Intersects(buttons[a].btn.rect) && enableButtons)
                {
                    activeButton = a;

                    //Change to other AppStates based on button press
                    switch (a)
                    {
                        case 0: //Home Button
                            state = AppState.Home;
                            break;
                        case 1: //Plan Button
                            state = AppState.Plan;
                            break;
                        case 2: //Cart Button
                            state = AppState.Cart;
                            break;
                        case 3: //Profile Button
                            state = AppState.Profile;
                            break;
                    }

                    break;
                }

            for (int a = 0; a < sliders.Count; a++)
                if (ms.LeftButton == ButtonState.Pressed && oldMs.LeftButton != ButtonState.Pressed && mouseRect.Intersects(sliders[a].point))
                {
                    enableSlider = true;
                    sliderID = a;
                }

            if (enableSlider)
            {
                if (ms.LeftButton == ButtonState.Pressed)
                    if (mouseRect.X >= sliders[sliderID].line.X && mouseRect.X <= sliders[sliderID].line.Right)
                    {
                        sliders[sliderID].point.X = mouseRect.X - 10;
                        sliders[sliderID].updateValue();
                    }
                    else
                        enableSlider = false;
            }
            
            switch (state)
            {
                case AppState.Cart: //0
                    if (ms.LeftButton == ButtonState.Pressed && oldMs.LeftButton != ButtonState.Pressed)
                        for (int a = 0; a < tickets[0].tickets.Count; a++)
                            if (mouseRect.Intersects(tickets[0].tickets[a].grab))
                            {
                                Ticket temp = tickets[0].tickets[a];
                                temp.movement = plus;
                                tickets[1].tickets.Add(temp);
                                tickets[0].tickets.RemoveAt(a);
                            }
                    break;
                case AppState.Plan: //1
                    if (ms.LeftButton == ButtonState.Pressed && oldMs.LeftButton != ButtonState.Pressed)
                        for (int a = 0; a < tickets[1].tickets.Count; a++)
                            if (mouseRect.Intersects(tickets[1].tickets[a].grab))
                            {
                                Ticket temp = tickets[1].tickets[a];
                                temp.movement = minus;
                                tickets[0].tickets.Add(temp);

                                cartValue += temp.cost;
                                cartTax = (double)((int)((cartValue * 0.0825) * 100)) / 100;
                                cartTotal = cartValue + cartTax;

                                outputs[23].str = "$" + cartValue;
                                outputs[24].str = "$" + cartTax;
                                outputs[25].str = "$" + cartTotal;

                                tickets[1].tickets.RemoveAt(a);
                            }
                    break;
                case AppState.Receipt: //2
                    break;
                case AppState.Home: //3
                    break;
            }
            
            for (int a = 0; a < themeBoxes.Count; a++)
                if (ms.LeftButton == ButtonState.Pressed && oldMs.LeftButton != ButtonState.Pressed && 
                    state == AppState.ChangeUI && mouseRect.Intersects(themeBoxes[a]))
                {
                    primaryTheme = themePrimaries[a];
                    secondaryTheme = themeSecondaries[a];
                }

            for (int a = 0; a < checks.Count; a++)
                if (ms.LeftButton == ButtonState.Pressed && oldMs.LeftButton != ButtonState.Pressed &&
                    (checks[a].app == state || checks[a].app == AppState.Any) && mouseRect.Intersects(checks[a].rect))
                {
                    checks[a].active = !checks[a].active;

                    if (a >= 1 && a <= 3 && checks[a].active)
                        for (int b = 1; b <= 3; b++)
                            if (a != b)
                                checks[b].active = false;
                }

            for (int a = 0; a < textButtons.Count; a++)
                if (ms.LeftButton == ButtonState.Pressed && oldMs.LeftButton != ButtonState.Pressed &&
                    (textButtons[a].app == state || textButtons[a].app == AppState.Any) && mouseRect.Intersects(textButtons[a].btn.rect))
                    switch (a)
                    {
                        case 2:
                            if (typedInput[0].Equals(user.email) && typedInput[1].Equals(user.getPassword()))
                            {
                                enableButtons = true;
                                state = AppState.Home;
                                activeButton = 0;
                            }
                            else
                                outputs[5].color = Color.Red;
                            break;
                        case 3:
                            if (!checks[0].active)
                            {
                                inputs[0].op.str = "Email: ";
                                inputs[1].op.str = "Password: ";
                                typedInput[0] = "";
                                typedInput[1] = "";
                            }

                            enableButtons = false;
                            outputs[5].color = Color.White;
                            state = AppState.Login;
                            activeButton = -1;
                            break;
                        case 4:
                            state = AppState.ChangeUI;
                            activeButton = -1;
                            break;
                        case 6:
                            state = AppState.AddFunds;
                            activeButton = -1;
                            break;
                        case 9:
                            checks[1].active = false;
                            checks[2].active = false;
                            checks[3].active = false;
                            state = AppState.PayInput;
                            activeButton = -1;
                            break;
                        case 10:
                        case 11:
                            if (!checks[1].active && !checks[2].active && !checks[3].active)
                                outputs[18].color = Color.Red;
                            else
                            {
                                outputs[18].color = Color.White;
                                outputs[26].color = Color.White;
                                state = AppState.PayConfirm;
                                activeButton = -1;
                            }
                            break;
                        case 12:
                            if (balance >= cartTotal || !checks[1].active)
                            {
                                while (tickets[0].tickets.Count > 0)
                                {
                                    Ticket temp = tickets[0].tickets[0];
                                    temp.movement = blank;
                                    for (int b = 2; b <= 3; b++)
                                        tickets[b].tickets.Add(temp);

                                    tickets[0].tickets.RemoveAt(0);
                                }

                                balance -= cartTotal;
                                balanceFloat = (balance * 100).ToString();
                                balanceFloat = balanceFloat.Insert(balanceFloat.Length - 2, ".");

                                outputs[14].str = "Current Balance: $" + balanceFloat;
                                outputs[32].str = "Current Wallet Balance: $" + balanceFloat;

                                cartValue = 0;
                                cartTax = 0;
                                cartTotal = 0;

                                outputs[23].str = "$" + cartValue;
                                outputs[24].str = "$" + cartTax;
                                outputs[25].str = "$" + cartTotal;

                                outputs[26].color = Color.White;
                                
                                state = AppState.Receipt;
                                activeButton = -1;
                            }
                            else
                                outputs[26].color = Color.Red;
                            break;
                        case 13:
                            state = AppState.Support;
                            activeButton = -1;
                            break;
                        case 18:
                            if (typedInput[7] != "")
                                balance += Convert.ToDouble(typedInput[7]) * 100;

                            balanceFloat = (balance * 100).ToString();
                            balanceFloat = balanceFloat.Insert(balanceFloat.Length - 2, ".");
                            outputs[14].str = "Current Balance: $" + balanceFloat;
                            outputs[32].str = "Current Wallet Balance: $" + balanceFloat;
                            break;
                    }

            for (int a = 0; a < imageButtons.Count; a++)
                if (ms.LeftButton == ButtonState.Pressed && oldMs.LeftButton != ButtonState.Pressed && enableButtons &&
                    (imageButtons[a].app == state || imageButtons[a].app == AppState.Any) && mouseRect.Intersects(imageButtons[a].png.rect))
                    switch(a)
                    {
                        case 1:
                            state = AppState.Settings;
                            activeButton = -1;
                            break;
                    }

            if (ms.LeftButton == ButtonState.Pressed && oldMs.LeftButton != ButtonState.Pressed)
            {
                enableInput = false;

                for (int a = 0; a < inputs.Count; a++)
                    if ((inputs[a].app == state || inputs[a].app == AppState.Any) && mouseRect.Intersects(inputs[a].rect))
                    {
                        enableInput = true;
                        inputID = a;
                    }
            }

            if (enableInput)
            {
                if (typedInput[inputID].Length < 25)
                {
                    for (int b = 65; b <= 90; b++)
                        if (kb.IsKeyDown((Keys)b) && !oldKb.IsKeyDown((Keys)b) && inputID != 7)
                        {
                            if (kb.IsKeyDown(Keys.LeftShift))
                                inputs[inputID].op.str += typeSorter(b, 0);

                            else
                                inputs[inputID].op.str += typeSorter(b, 1);
                        }

                    for (int b = 48; b <= 57; b++)
                        if (kb.IsKeyDown((Keys)b) && !oldKb.IsKeyDown((Keys)b))
                        {
                            if (kb.IsKeyDown(Keys.LeftShift))
                                inputs[inputID].op.str += typeSorter(b, 2);

                            else
                                inputs[inputID].op.str += typeSorter(b, 3);
                        }

                    if (kb.IsKeyDown(Keys.OemPeriod) && !oldKb.IsKeyDown(Keys.OemPeriod))
                        inputs[inputID].op.str += typeSorter(-1, 4);

                    if (kb.IsKeyDown(Keys.Space) && !oldKb.IsKeyDown(Keys.Space) && inputID != 7)
                        inputs[inputID].op.str += typeSorter(-1, 5);
                }

                if (kb.IsKeyDown((Keys)8) && !oldKb.IsKeyDown((Keys)8) && typedInput[inputID].Length > 0)
                {
                    inputs[inputID].op.str = inputs[inputID].op.str.Substring(0, inputs[inputID].op.str.Length - 1);
                    typedInput[inputID] = typedInput[inputID].Substring(0, typedInput[inputID].Length - 1);
                }

                if (inputID == 4)
                    user.email = inputs[4].op.str;
            }

            if (oldState == AppState.Receipt && state != AppState.Receipt)
                tickets[2].tickets = new List<Ticket>();

            oldKb = Keyboard.GetState();
            oldMs = Mouse.GetState();
            oldState = state;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.White);
            this.spriteBatch.Begin();

            //Top Ribbon
            spriteBatch.Draw(blank, new Rectangle(0, 0, 360, 60), primaryTheme);

            //Navigation Buttons
            for (int a = 0; a < buttons.Count; a++)
                if(buttons[a].app == state || buttons[a].app == AppState.Any)
                {
                    if (activeButton == a)
                    {
                        spriteBatch.Draw(buttons[a].btn.text, buttons[a].btn.rect, primaryTheme);
                        spriteBatch.Draw(buttons[a].png.text, buttons[a].png.rect, Color.Black);
                        spriteBatch.DrawString(buttons[a].op.font, buttons[a].op.str, buttons[a].op.vec, Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(buttons[a].btn.text, buttons[a].btn.rect, secondaryTheme);
                        spriteBatch.Draw(buttons[a].png.text, buttons[a].png.rect, Color.Black);
                        spriteBatch.DrawString(buttons[a].op.font, buttons[a].op.str, buttons[a].op.vec, Color.Black);
                    }
                }

            //Empty Container Images
            for (int a = 0; a < containers.Count; a++)
                if (containers[a].app == state || containers[a].app == AppState.Any)
                {
                    spriteBatch.Draw(blank, containers[a].rect, Color.Black);
                    spriteBatch.Draw(blank, new Rectangle(containers[a].rect.X + 1, containers[a].rect.Y + 1, containers[a].rect.Width - 2, containers[a].rect.Height - 2), Color.White);
                }

            //Image Buttons
            for (int a = 0; a < imageButtons.Count; a++)
                if (imageButtons[a].app == state || imageButtons[a].app == AppState.Any)
                    spriteBatch.Draw(imageButtons[a].png.text, imageButtons[a].png.rect, Color.Black);

            //User Input Boxes
            for (int a = 0; a < inputs.Count; a++)
                if (inputs[a].app == state || inputs[a].app == AppState.Any)
                {
                    spriteBatch.Draw(inputs[a].text, inputs[a].rect, Color.Black);
                    spriteBatch.Draw(inputs[a].text,
                        new Rectangle(inputs[a].rect.X + 1, inputs[a].rect.Y + 1, inputs[a].rect.Width - 2, inputs[a].rect.Height - 2),
                        new Color(240, 240, 240));
                    spriteBatch.DrawString(inputs[a].op.font, inputs[a].op.str, inputs[a].op.vec, inputs[a].op.color);
                }

            //Text Output
            for (int a = 0; a < outputs.Count; a++)
                if (outputs[a].app == state || outputs[a].app == AppState.Any)
                    spriteBatch.DrawString(outputs[a].font, outputs[a].str, outputs[a].vec, outputs[a].color);

            //Sliders for User to Set Values
            for (int a = 0; a < sliders.Count; a++)
                if (sliders[a].app == state || sliders[a].app == AppState.Any)
                {
                    spriteBatch.Draw(blank, sliders[a].line, Color.LightGray);
                    spriteBatch.Draw(circle, sliders[a].point, primaryTheme);
                    spriteBatch.DrawString(small, sliders[a].name.str, sliders[a].name.vec, Color.Black);
                    spriteBatch.DrawString(small, sliders[a].value.str, sliders[a].value.vec, Color.Black);
                }

            //Clickable Buttons (Text Only)
            for (int a = 0; a < textButtons.Count; a++)
                if (textButtons[a].app == state || textButtons[a].app == AppState.Any)
                {
                    spriteBatch.Draw(textButtons[a].btn.text, textButtons[a].btn.rect, primaryTheme);
                    spriteBatch.Draw(textButtons[a].btn.text,
                        new Rectangle(textButtons[a].btn.rect.X + 1, textButtons[a].btn.rect.Y + 1, textButtons[a].btn.rect.Width - 2, textButtons[a].btn.rect.Height - 2),
                        secondaryTheme);
                    spriteBatch.DrawString(textButtons[a].op.font, textButtons[a].op.str, textButtons[a].op.vec, textButtons[a].op.color);
                }

            //Ticket Lists for User Moving
            for (int a = 0; a < tickets.Count; a++)
                if (tickets[a].app == state || tickets[a].app == AppState.Any)
                {
                    spriteBatch.Draw(blank, tickets[a].box, Color.Black);
                    spriteBatch.Draw(blank, new Rectangle(tickets[a].box.X + 1, tickets[a].box.Y + 1, tickets[a].box.Width - 2, tickets[a].box.Height - 2), Color.White);
                    int lastBoxY = tickets[a].box.Y;

                    for (int b = 0; b < tickets[a].tickets.Count; b++)
                    {
                        if (!sliders[0].value.str.Equals("Any") && tickets[a].tickets[b].cost > Convert.ToDouble(sliders[0].value.str.Split(" ")[0]))
                            continue;
                        if (!sliders[1].value.str.Equals("Any") && tickets[a].tickets[b].time > Convert.ToDouble(sliders[1].value.str.Split(" ")[0]))
                            continue;
                        if (!sliders[2].value.str.Equals("Any") && tickets[a].tickets[b].distance > Convert.ToDouble(sliders[2].value.str.Split(" ")[0]))
                            continue;
                        if (!typedInput[6].ToLower().Equals(tickets[a].tickets[b].name.Substring(0, typedInput[6].Length).ToLower()))
                            continue;

                        spriteBatch.Draw(blank, new Rectangle(35, lastBoxY, tickets[a].box.Width, 40), Color.Black);
                        spriteBatch.Draw(blank, new Rectangle(36, lastBoxY + 1, tickets[a].box.Width - 2, 38), Color.White);
                        tickets[a].tickets[b].setGrab(new Rectangle(tickets[a].box.Right - 40, lastBoxY + 5, 30, 30));

                        spriteBatch.Draw(tickets[a].tickets[b].movement, tickets[a].tickets[b].grab, Color.White);
                        spriteBatch.DrawString(small, tickets[a].tickets[b].name, new Vector2(40, lastBoxY + 5), Color.Black);
                        lastBoxY += 39;
                    }
                }

            //Check Boxes
            for (int a = 0; a < checks.Count; a++)
                if (checks[a].app == state || checks[a].app == AppState.Any)
                {
                    spriteBatch.Draw(checks[a].text, checks[a].rect, checks[a].color);
                    spriteBatch.Draw(checks[a].text,
                        new Rectangle(checks[a].rect.X + 1, checks[a].rect.Y + 1, checks[a].rect.Width - 2, checks[a].rect.Height - 2),
                        Color.White);

                    if (checks[a].active)
                        spriteBatch.Draw(checks[a].text,
                            new Rectangle(checks[a].rect.X + 3, checks[a].rect.Y + 3, checks[a].rect.Width - 6, checks[a].rect.Height - 6),
                            new Color(100, 100, 100));
                }

            //Icon Images
            for (int a = 0; a < images.Count; a++)
                if (images[a].app == state || images[a].app == AppState.Any)
                    spriteBatch.Draw(images[a].text, images[a].rect, Color.White);

            if (state == AppState.ChangeUI)
                for (int a = 0; a < themeBoxes.Count; a++)
                {
                    spriteBatch.Draw(blank, themeBoxes[a], Color.Black);
                    spriteBatch.Draw(blank, new Rectangle(themeBoxes[a].X + 1, themeBoxes[a].Y + 1, themeBoxes[a].Width - 2, themeBoxes[a].Height - 2), themePrimaries[a]);
                }

            this.spriteBatch.End();
            base.Draw(gameTime);
        }

        public char typeSorter(int b, int i)
        {
            switch (i)
            {
                case 0:
                case 3:
                    if (inputID != 1)
                        toOutput = Convert.ToChar(b);
                    else
                        toOutput = Convert.ToChar(42);

                    typedInput[inputID] += Convert.ToChar(b);
                    break;
                case 1:
                    if (inputID != 1)
                        toOutput = Convert.ToChar(b + 32);
                    else
                        toOutput = Convert.ToChar(42);

                    typedInput[inputID] += Convert.ToChar(b + 32);
                    break;
                case 2:
                    if (inputID != 1)
                    {
                        switch (b)
                        {
                            case 48: //0 --> )
                                toOutput = Convert.ToChar(41);
                                typedInput[inputID] += Convert.ToChar(41);
                                break;
                            case 49: //1 --> !
                                toOutput = Convert.ToChar(33);
                                typedInput[inputID] += Convert.ToChar(33);
                                break;
                            case 50: //2 --> @
                                toOutput = Convert.ToChar(64);
                                typedInput[inputID] += Convert.ToChar(64);
                                break;
                            case 51: //3 --> #
                                toOutput = Convert.ToChar(35);
                                typedInput[inputID] += Convert.ToChar(35);
                                break;
                            case 52: //4 --> $
                                toOutput = Convert.ToChar(36);
                                typedInput[inputID] += Convert.ToChar(36);
                                break;
                            case 53: //5 --> %
                                toOutput = Convert.ToChar(37);
                                typedInput[inputID] += Convert.ToChar(37);
                                break;
                            case 54: //6 --> ^
                                toOutput = Convert.ToChar(94);
                                typedInput[inputID] += Convert.ToChar(94);
                                break;
                            case 55: //7 --> &
                                toOutput = Convert.ToChar(38);
                                typedInput[inputID] += Convert.ToChar(38);
                                break;
                            case 56: //8 --> *
                                toOutput = Convert.ToChar(42);
                                typedInput[inputID] += Convert.ToChar(42);
                                break;
                            case 57: //9 --> (
                                toOutput = Convert.ToChar(40);
                                typedInput[inputID] += Convert.ToChar(40);
                                break;
                        }
                    }
                    else
                    {
                        toOutput = Convert.ToChar(42);
                        switch (b)
                        {
                            case 48: //0 --> )
                                typedInput[inputID] += Convert.ToChar(41);
                                break;
                            case 49: //1 --> !
                                typedInput[inputID] += Convert.ToChar(33);
                                break;
                            case 50: //2 --> @
                                typedInput[inputID] += Convert.ToChar(64);
                                break;
                            case 51: //3 --> #
                                typedInput[inputID] += Convert.ToChar(35);
                                break;
                            case 52: //4 --> $
                                typedInput[inputID] += Convert.ToChar(36);
                                break;
                            case 53: //5 --> %
                                typedInput[inputID] += Convert.ToChar(37);
                                break;
                            case 54: //6 --> ^
                                typedInput[inputID] += Convert.ToChar(94);
                                break;
                            case 55: //7 --> &
                                typedInput[inputID] += Convert.ToChar(38);
                                break;
                            case 56: //8 --> *
                                typedInput[inputID] += Convert.ToChar(42);
                                break;
                            case 57: //9 --> (
                                typedInput[inputID] += Convert.ToChar(40);
                                break;
                        }
                    }
                    break;
                case 4:
                    if (inputID != 1)
                        toOutput = '.';
                    else
                        toOutput = Convert.ToChar(42);

                    typedInput[inputID] += '.';
                    break;
                case 5:
                    if (inputID != 1)
                        toOutput = ' ';
                    else
                        toOutput = Convert.ToChar(42);

                    typedInput[inputID] += ' ';
                    break;
            }

            return toOutput;
        }

        public class AppUser
        {
            public string name;
            public string email;
            public string phoneNumber;
            public string address;
            private string password;

            public AppUser(string n, string e, string p, string a, string s)
            {
                this.name = n;
                this.email = e;
                this.phoneNumber = p;
                this.address = a;
                this.password = s;
            }

            public string getPassword()
            {
                return password;
            }

            public void setPassword(string p)
            {
                password = p;
            }
        }

        public class Slider
        {
            public Rectangle line;
            public Rectangle point;
            public Output name;
            public Output value;
            public string units;
            public int maximum;
            public AppState app;

            public Slider(Output n, string u, int m, SpriteFont f, AppState a)
            {
                this.name = n;
                this.units = u;
                this.maximum = m;
                this.app = a;
                this.line = new Rectangle(100, (int)name.vec.Y + 3, 150, 10);
                this.point = new Rectangle(line.Right - 10, line.Y - 5, 20, 20);
                this.value = new Output(f, new Vector2(line.Right + 30, name.vec.Y), "Any", Color.Black);
            }

            public void updateValue()
            {
                double percentage = (double)(point.X + 11 - line.X) / (double)(line.Width - 1);
                double conversion = (double)maximum * percentage;

                if (percentage >= 1)
                    value.str = "Any";
                else
                    value.str = ((int)(conversion * 100) / 100) + " " + units;
            }
        }

        public class TicketList
        {
            public Rectangle box;
            public SpriteFont font;
            public AppState app;
            public List<Ticket> tickets;

            public TicketList(Rectangle b, SpriteFont f, AppState a)
            {
                this.box = b;
                this.font = f;
                this.app = a;
                this.tickets = new List<Ticket>();
            }
        }

        public class Ticket
        {
            public string name;
            public double cost;
            public double distance;
            public double time;
            public Texture2D movement;
            public Rectangle grab;

            public Ticket(string n, double c, double d, double t, Texture2D m)
            {
                this.name = n;
                this.cost = c;
                this.distance = d;
                this.time = t;
                this.movement = m;
            }

            public void setGrab(Rectangle g)
            {
                this.grab = g;
            }
        }

        public class Container
        {
            public Rectangle rect;
            public AppState app;

            public Container(Rectangle r, AppState a)
            {
                this.rect = r;
                this.app = a;
            }
        }

        public class Button
        {
            public Image btn;
            public Image png;
            public Output op;
            public AppState app;

            public Button(Image b, Image p, Output o, AppState a)
            {
                this.btn = b;
                this.png = p;
                this.op = o;
                this.app = a;
            }
        }

        public class Image
        {
            public Texture2D text;
            public Rectangle rect;
            public AppState app;

            public Image(Texture2D t, Rectangle r)
            {
                this.text = t;
                this.rect = r;
            }

            public Image(Texture2D t, Rectangle r, AppState a)
            {
                this.text = t;
                this.rect = r;
                this.app = a;
            }
        }

        public class Output
        {
            public SpriteFont font;
            public Vector2 vec;
            public string str;
            public Color color;
            public AppState app;

            public Output(SpriteFont f, Vector2 v, string s, Color c)
            {
                this.font = f;
                this.vec = v;
                this.str = s;
                this.color = c;
            }

            public Output(SpriteFont f, Vector2 v, string s, Color c, AppState a)
            {
                this.font = f;
                this.vec = v;
                this.str = s;
                this.color = c;
                this.app = a;
            }
        }

        public class TextBox
        {
            public Texture2D text;
            public Rectangle rect;
            public Output op;
            public string verify;
            public AppState app;

            public TextBox(Texture2D t, Rectangle r, Output o, string v)
            {
                this.text = t;
                this.rect = r;
                this.op = o;
                this.verify = v;
            }

            public TextBox(Texture2D t, Rectangle r, Output o, string v, AppState a)
            {
                this.text = t;
                this.rect = r;
                this.op = o;
                this.verify = v;
                this.app = a;
            }
        }

        public class TextButton
        {
            public Image btn;
            public Output op;
            public AppState app;

            public TextButton(Image b, Output o)
            {
                this.btn = b;
                this.op = o;
            }

            public TextButton(Image b, Output o, AppState a)
            {
                this.btn = b;
                this.op = o;
                this.app = a;
            }
        }

        public class ImageButton
        {
            public Image png;
            public AppState app;

            public ImageButton(Image p)
            {
                this.png = p;
            }

            public ImageButton(Image p, AppState a)
            {
                this.png = p;
                this.app = a;
            }
        }

        public class CheckBox
        {
            public Texture2D text;
            public Rectangle rect;
            public Color color;
            public bool active;
            public AppState app;

            public CheckBox(Texture2D t, Rectangle r, Color c, bool b)
            {
                this.text = t;
                this.rect = r;
                this.color = c;
                this.active = b;
            }

            public CheckBox(Texture2D t, Rectangle r, Color c, bool b, AppState a)
            {
                this.text = t;
                this.rect = r;
                this.color = c;
                this.active = b;
                this.app = a;
            }
        }

        public enum AppState
        {
            Home,
            Cart,
            Plan,
            Profile,
            Any,
            AddFunds,
            PayConfirm,
            PayInput,
            Login,
            Settings,
            Support,
            Receipt,
            ChangeUI
        }
    }
}