#nullable enable
using Newtonsoft.Json;

namespace onboard.util;

/**
 * A request to the Devcade API.
 */
public class Request {
    private static uint _id;

    public enum RequestType {
        Ping,
        
        GetGameList,
        GetGameListFromFs,
        GetGame,
        DownloadGame,
        DownloadIcon,
        DownloadBanner,
        
        GetTagList,
        GetTag,
        GetGameListFromTag,
        
        GetUser,

        SetProduction,

        LaunchGame,
    }

    public uint id { get; private set; }
    private readonly RequestType type;
    private readonly object data;

    private Request(RequestType type, string? string_id = null, bool? prod = null) {
        this.id = _id++;
        this.type = type;
        this.data = type switch {
            RequestType.Ping or RequestType.GetGameList or RequestType.GetGameListFromFs or RequestType.GetTagList =>
                id,
            RequestType.SetProduction => new object[] { id, prod ?? true },
            _ => new object[] { id, string_id ?? "" }
        };
    }

    public string serialize() {
        return "{\"type\": \"" + type + "\", \"data\": " + JsonConvert.SerializeObject(data) + "}";
    }

    public static Request GetGameList() {
        return new Request(RequestType.GetGameList);
    }

    public static Request GetGameListFromFs() {
        return new Request(RequestType.GetGameListFromFs);
    }

    public static Request GetGame(string game_id) {
        return new Request(RequestType.GetGame, game_id);
    }

    public static Request DownloadGame(string game_id) {
        return new Request(RequestType.DownloadGame, game_id);
    }

    public static Request DownloadIcon(string game_id) {
        return new Request(RequestType.DownloadIcon, game_id);
    }

    public static Request DownloadBanner(string game_id) {
        return new Request(RequestType.DownloadBanner, game_id);
    }

    public static Request LaunchGame(string game_id) {
        return new Request(RequestType.LaunchGame, game_id);
    }

    public static Request SetProduction(bool prod) {
        return new Request(RequestType.SetProduction, null, prod);
    }
    
    public static Request GetTagList() {
        return new Request(RequestType.GetTagList);
    }
    
    public static Request GetTag(string tag_name) {
        return new Request(RequestType.GetTag, tag_name);
    }
    
    public static Request GetGameListFromTag(string tag_name) {
        return new Request(RequestType.GetGameListFromTag, tag_name);
    }
    
    public static Request getUser(string username) {
        return new Request(RequestType.GetUser, username);
    }
    
    public static Request Ping() {
        return new Request(RequestType.Ping);
    }
}