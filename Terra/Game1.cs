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
        private List<string> typedInput;

        //App Variables
        public AppState state;
        public bool enableButtons;
        public bool enableInput;
        public int inputID;
        public char toOutput;
        public AppUser user;

        //User Variables

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
            enableButtons = false;
            enableInput = false;
            inputID = -1;
            user = new AppUser("Firstname Lastname", "fml100000@utdallas.edu", "123-456-7890", "1234 Main St", "Password");
            toOutput = new char();

            buttons = new List<Button>();
            images = new List<Image>();
            outputs = new List<Output>();
            inputs = new List<TextBox>();
            textButtons = new List<TextButton>();
            imageButtons = new List<ImageButton>();
            checks = new List<CheckBox>();
            typedInput = new List<string>();

            header = this.Content.Load<SpriteFont>("font"); //Header Style
            prompt = this.Content.Load<SpriteFont>("prompt"); //Prompt Style
            small = this.Content.Load<SpriteFont>("small"); //Small Style

            blank = this.Content.Load<Texture2D>("blank");
            bell = this.Content.Load<Texture2D>("bell");
            gear = this.Content.Load<Texture2D>("gear");

            home = this.Content.Load<Texture2D>("home");
            plan = this.Content.Load<Texture2D>("plan");
            cart = this.Content.Load<Texture2D>("cart");
            profile = this.Content.Load<Texture2D>("profile");

            //Any AppState UI
            buttons.Add(new Button(
                new Image(blank, new Rectangle(1, 651, 88, 88), Color.LightGreen),
                new Image(home, new Rectangle(30, 666, 30, 30), Color.Black),
                new Output(small, new Vector2(28, 706), "Home", Color.Black), AppState.Any));
            buttons.Add(new Button(
                new Image(blank, new Rectangle(91, 651, 88, 88), Color.LightGreen),
                new Image(plan, new Rectangle(110, 656, 50, 50), Color.Black),
                new Output(small, new Vector2(122, 706), "Plan", Color.Black), AppState.Any));
            buttons.Add(new Button(
                new Image(blank, new Rectangle(181, 651, 88, 88), Color.LightGreen),
                new Image(cart, new Rectangle(210, 666, 30, 30), Color.Black),
                new Output(small, new Vector2(214, 706), "Cart", Color.Black), AppState.Any));
            buttons.Add(new Button(
                new Image(blank, new Rectangle(271, 651, 88, 88), Color.LightGreen),
                new Image(profile, new Rectangle(300, 666, 30, 30), Color.Black),
                new Output(small, new Vector2(298, 706), "Profile", Color.Black), AppState.Any));
            imageButtons.Add(new ImageButton(new Image(bell, new Rectangle(270, 15, 30, 30), Color.Black), AppState.Any));
            imageButtons.Add(new ImageButton(new Image(gear, new Rectangle(315, 15, 30, 30), Color.Black), AppState.Any));
            outputs.Add(new Output(header, new Vector2(35, 20), "Ticket Maestro", Color.White, AppState.Any));

            //Login AppState UI
            outputs.Add(new Output(prompt, new Vector2(35, 100), "Sign In", Color.Black, AppState.Login));
            outputs.Add(new Output(prompt, new Vector2(35, 490), "Forgot Password?", Color.Black, AppState.Login));
            outputs.Add(new Output(prompt, new Vector2(35, 565), "No Account?", Color.Black, AppState.Login));
            outputs.Add(new Output(small, new Vector2(230, 240), "Remember Me", Color.Black, AppState.Login));
            outputs.Add(new Output(small, new Vector2(65, 330), "Incorrect email or password, try again.", Color.White, AppState.Login));
            inputs.Add(new TextBox(blank, new Rectangle(35, 130, 290, 40),
                new Output(small, new Vector2(40, 145), "Email: ", Color.Black), user.email, AppState.Login));
            inputs.Add(new TextBox(blank, new Rectangle(35, 180, 290, 40),
                new Output(small, new Vector2(40, 195), "Password: ", Color.Black), user.getPassword(), AppState.Login));
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(35, 515, 290, 30), Color.DarkGreen),
                new Output(small, new Vector2(130, 523), "Password Reset", Color.Black), AppState.Login));
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(35, 590, 290, 30), Color.DarkGreen),
                new Output(small, new Vector2(154, 598), "Sign Up", Color.Black), AppState.Login));
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(205, 270, 110, 30), Color.DarkGreen),
                new Output(small, new Vector2(245, 278), "Login", Color.Black), AppState.Login));
            checks.Add(new CheckBox(blank, new Rectangle(205, 238, 20, 20), Color.Black, false, AppState.Login));

            //Profile AppState UI
            outputs.Add(new Output(prompt, new Vector2(35, 100), "Name", Color.Black, AppState.Profile));
            outputs.Add(new Output(prompt, new Vector2(35, 220), "Phone Number", Color.Black, AppState.Profile));
            outputs.Add(new Output(prompt, new Vector2(35, 340), "Email Address", Color.Black, AppState.Profile));
            outputs.Add(new Output(prompt, new Vector2(35, 460), "Address", Color.Black, AppState.Profile));
            inputs.Add(new TextBox(blank, new Rectangle(35, 130, 290, 40),
                new Output(small, new Vector2(40, 145), user.name, Color.Black), user.email, AppState.Profile));
            inputs.Add(new TextBox(blank, new Rectangle(35, 250, 290, 40),
                new Output(small, new Vector2(40, 265), user.phoneNumber, Color.Black), user.email, AppState.Profile));
            inputs.Add(new TextBox(blank, new Rectangle(35, 370, 290, 40),
                new Output(small, new Vector2(40, 385), user.email, Color.Black), user.email, AppState.Profile));
            inputs.Add(new TextBox(blank, new Rectangle(35, 490, 290, 40),
                new Output(small, new Vector2(40, 505), user.address, Color.Black), user.email, AppState.Profile));
            
            //Settings AppState UI
            images.Add(new Image(profile, new Rectangle(130, 85, 100, 100), Color.Black, AppState.Settings));
            outputs.Add(new Output(prompt, new Vector2(128, 190), "Edit Settings", Color.Black, AppState.Settings));
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(55, 245, 250, 30), Color.DarkGreen),
                new Output(small, new Vector2(160, 253), "Logout", Color.Black), AppState.Settings));
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(55, 290, 250, 30), Color.DarkGreen),
                new Output(small, new Vector2(138, 298), "Change Theme", Color.Black), AppState.Settings));
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(55, 335, 250, 30), Color.DarkGreen),
                new Output(small, new Vector2(125, 343), "Toggle Notifications", Color.Black), AppState.Settings));
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(55, 380, 250, 30), Color.DarkGreen),
                new Output(small, new Vector2(107, 388), "Manage Payment Options", Color.Black), AppState.Settings));
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(55, 425, 250, 30), Color.DarkGreen),
                new Output(small, new Vector2(128, 433), "Change Language", Color.Black), AppState.Settings));
            textButtons.Add(new TextButton(new Image(blank, new Rectangle(55, 470, 250, 30), Color.DarkGreen),
                new Output(small, new Vector2(160, 478), "About", Color.Black), AppState.Settings));

            //Default Input Values
            typedInput.Add("");
            typedInput.Add("");
            typedInput.Add(user.name);
            typedInput.Add(user.phoneNumber);
            typedInput.Add(user.email);
            typedInput.Add(user.address);

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
                    buttons[a].btn.color = Color.DarkGreen;
                    buttons[a].png.color = Color.White;
                    buttons[a].op.color = Color.White;

                    for (int b = 0; b < buttons.Count; b++)
                        if (b != a)
                        {
                            buttons[b].btn.color = Color.LightGreen;
                            buttons[b].png.color = Color.Black;
                            buttons[b].op.color = Color.Black;
                        }

                    //Change to other AppStates based on button press
                    switch (a)
                    {
                        case 0: //Home Button
                            state = AppState.Home;
                            break;
                        case 1: //Cart Button
                            state = AppState.Cart;
                            break;
                        case 2: //Plan Button
                            state = AppState.Plan;
                            break;
                        case 3: //Profile Button
                            state = AppState.Profile;
                            break;
                    }

                    break;
                }

            for (int a = 0; a < checks.Count; a++)
                if (ms.LeftButton == ButtonState.Pressed && oldMs.LeftButton != ButtonState.Pressed &&
                    (checks[a].app == state || checks[a].app == AppState.Any) && mouseRect.Intersects(checks[a].rect))
                    checks[a].active = !checks[a].active;

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
                                buttons[0].btn.color = Color.DarkGreen;
                                buttons[0].png.color = Color.White;
                                buttons[0].op.color = Color.White;
                            }
                            else
                                outputs[5].color = Color.Red;
                            break;
                        case 3:
                            inputs[0].op.str = "Email: ";
                            inputs[1].op.str = "Password: ";
                            typedInput[0] = "";
                            typedInput[1] = "";
                            enableButtons = false;
                            outputs[5].color = Color.White;
                            state = AppState.Login;

                            for (int b = 0; b < buttons.Count; b++)
                            {
                                buttons[b].btn.color = Color.LightGreen;
                                buttons[b].png.color = Color.Black;
                                buttons[b].op.color = Color.Black;
                            }
                            break;
                    }

            for (int a = 0; a < imageButtons.Count; a++)
                if (ms.LeftButton == ButtonState.Pressed && oldMs.LeftButton != ButtonState.Pressed && enableButtons &&
                    (imageButtons[a].app == state || imageButtons[a].app == AppState.Any) && mouseRect.Intersects(imageButtons[a].png.rect))
                    switch(a)
                    {
                        case 1:
                            state = AppState.Settings;
                            for (int b = 0; b < buttons.Count; b++)
                            {
                                buttons[b].btn.color = Color.LightGreen;
                                buttons[b].png.color = Color.Black;
                                buttons[b].op.color = Color.Black;
                            }
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
                        if (kb.IsKeyDown((Keys)b) && !oldKb.IsKeyDown((Keys)b))
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

                    if (kb.IsKeyDown(Keys.Space) && !oldKb.IsKeyDown(Keys.Space))
                        inputs[inputID].op.str += typeSorter(-1, 5);
                }

                if (kb.IsKeyDown((Keys)8) && !oldKb.IsKeyDown((Keys)8) && typedInput[inputID].Length > 0)
                {
                    inputs[inputID].op.str = inputs[inputID].op.str.Substring(0, inputs[inputID].op.str.Length - 1);
                    typedInput[inputID] = typedInput[inputID].Substring(0, typedInput[inputID].Length - 1);
                }
            }

            oldKb = Keyboard.GetState();
            oldMs = Mouse.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.White);
            this.spriteBatch.Begin();

            //Top Ribbon
            spriteBatch.Draw(blank, new Rectangle(0, 0, 360, 60), Color.DarkGreen);

            //Navigation Buttons
            for (int a = 0; a < buttons.Count; a++)
            {
                spriteBatch.Draw(buttons[a].btn.text, buttons[a].btn.rect, buttons[a].btn.color);
                spriteBatch.Draw(buttons[a].png.text, buttons[a].png.rect, buttons[a].png.color);
                spriteBatch.DrawString(buttons[a].op.font, buttons[a].op.str, buttons[a].op.vec, buttons[a].op.color);
            }

            //Icon Images
            for (int a = 0; a < images.Count; a++)
                if (images[a].app == state || images[a].app == AppState.Any)
                    spriteBatch.Draw(images[a].text, images[a].rect, images[a].color);

            //Image Buttons
            for (int a = 0; a < imageButtons.Count; a++)
                if (imageButtons[a].app == state || imageButtons[a].app == AppState.Any)
                    spriteBatch.Draw(imageButtons[a].png.text, imageButtons[a].png.rect, imageButtons[a].png.color);

            //Text Output
            for (int a = 0; a < outputs.Count; a++)
                if (outputs[a].app == state || outputs[a].app == AppState.Any)
                    spriteBatch.DrawString(outputs[a].font, outputs[a].str, outputs[a].vec, outputs[a].color);

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

            //Clickable Buttons (Text Only)
            for (int a = 0; a < textButtons.Count; a++)
                if (textButtons[a].app == state || textButtons[a].app == AppState.Any)
                {
                    spriteBatch.Draw(textButtons[a].btn.text, textButtons[a].btn.rect, textButtons[a].btn.color);
                    spriteBatch.Draw(textButtons[a].btn.text,
                        new Rectangle(textButtons[a].btn.rect.X + 1, textButtons[a].btn.rect.Y + 1, textButtons[a].btn.rect.Width - 2, textButtons[a].btn.rect.Height - 2),
                        Color.LightGreen);
                    spriteBatch.DrawString(textButtons[a].op.font, textButtons[a].op.str, textButtons[a].op.vec, textButtons[a].op.color);
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
            public Color color;
            public AppState app;

            public Image(Texture2D t, Rectangle r, Color c)
            {
                this.text = t;
                this.rect = r;
                this.color = c;
            }

            public Image(Texture2D t, Rectangle r, Color c, AppState a)
            {
                this.text = t;
                this.rect = r;
                this.color = c;
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
            Ticket, //Home
            Search, //Plan
            AddFunds, //Cart
            Confirmation, //Cart
            Payment, //Cart
            Login, //Profile
            Settings, //Profile
            Support //Profile
        }
    }
}