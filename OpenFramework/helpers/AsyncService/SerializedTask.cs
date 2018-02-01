namespace OpenFramework.Helper
{
    [System.Serializable]
    public class SerializedTask
    {
        public string id;
        public string type;
        public string data;
    }
}

/*
CheckToken(
    OnValid     +=  GetProfile(
                        OnComplete  +=  GetData()
                        OnError     +=  GetProfile()
                    )   
    OnNoToken   +=  Register(
                        OnComplete  +=  InitializeProfile(
                                        Oncomplete += 
                                    )
                        OnError     +=  ErroFaildToLoadGame()
                    )
    OnExpired   +=  GetNewToken()
)

*/
