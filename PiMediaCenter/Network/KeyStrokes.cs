using PiMediaCenter.Network.Comm;

namespace PiMediaCenter.Network
{
    class KeyStrokes
    {
        MainClient client;
        public KeyStrokes()
        {
            client = new MainClient("KeyStrokes", onDataRecieve);
        }
        public void KeyUp()
        {
            sent("keyup");
        }
        public void KeyDown()
        {
            sent("keydown");
        }
        public void KeyLeft()
        {
            sent("keyleft");
        }
        public void KeyRight()
        {
            sent("keyright");
        }
        public void KeyEnter()
        {
            sent("keyenter");
        }
        public void KeyBack()
        {
            sent("keyback");
        }
        public void MouseRight()
        {
            sent("mouseright");
        }
        public void Text(string text)
        {
            DataCodex code = new DataCodex("KeyStrokes");
            code.put("call", "text");
            code.put("text", text);
            client.sentData(code);
        }
        public void Shutdown()
        {
            sent("shutdown");
        }
        void sent(string call)
        {
            DataCodex code = new DataCodex("KeyStrokes");
            code.put("call", call);
            client.sentData(code);
        }
        void onDataRecieve(DataCodex code)
        {

        }

    }
}
