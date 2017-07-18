namespace MyWeChatTool.Model
{
    public class WeChatMenu
    {
        public menu menu;
        public conditionalmenu[] conditionalmenu;
    }

    public class menu
    {
        public Button[] button;
        public string menuid;
    }

    public class conditionalmenu
    {
        public Button[] button;
        public matchrule matchrule;
        public string menuid;
    }

    public class Button : SubButton
    {
        public SubButton[] sub_button;
    }

    public class SubButton
    {
        public string type;
        public string name;
        public string key;
        public string url;
    }

    public class matchrule
    {
        public string group_id;
    }
}
