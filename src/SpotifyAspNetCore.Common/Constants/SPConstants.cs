using System.Runtime.Serialization;

namespace SpotifyAspNetCore.Common
{
	public enum ReturnCodes : int
        {
            Success = 0,
            InvalidClientId,
            InvalidClientSecret,
            OperationFailed,
            InvalidArgument
        }

    /// <summary>
	/// Error codes.
	/// </summary>
	public enum SP_ERROR
    {
		SP_ERROR_OK                        = 0,  ///< No errors encountered
		SP_ERROR_BAD_API_VERSION           = 1,  ///< The library version targeted does not match the one you claim you support
		SP_ERROR_API_INITIALIZATION_FAILED = 2,  ///< Initialization of library failed - are cache locations etc. valid?
		SP_ERROR_TRACK_NOT_PLAYABLE        = 3,  ///< The track specified for playing cannot be played
		SP_ERROR_BAD_APPLICATION_KEY       = 5,  ///< The application key is invalid
		SP_ERROR_BAD_USERNAME_OR_PASSWORD  = 6,  ///< Login failed because of bad username and/or password
		SP_ERROR_USER_BANNED               = 7,  ///< The specified username is banned
		SP_ERROR_UNABLE_TO_CONTACT_SERVER  = 8,  ///< Cannot connect to the Spotify backend system
		SP_ERROR_CLIENT_TOO_OLD            = 9,  ///< Client is too old, library will need to be updated
		SP_ERROR_OTHER_PERMANENT           = 10, ///< Some other error occurred, and it is permanent (e.g. trying to relogin will not help)
		SP_ERROR_BAD_USER_AGENT            = 11, ///< The user agent string is invalid or too long
		SP_ERROR_MISSING_CALLBACK          = 12, ///< No valid callback registered to handle events
		SP_ERROR_INVALID_INDATA            = 13, ///< Input data was either missing or invalid
		SP_ERROR_INDEX_OUT_OF_RANGE        = 14, ///< Index out of range
		SP_ERROR_USER_NEEDS_PREMIUM        = 15, ///< The specified user needs a premium account
		SP_ERROR_OTHER_TRANSIENT           = 16, ///< A transient error occurred.
		SP_ERROR_IS_LOADING                = 17, ///< The resource is currently loading
		SP_ERROR_NO_STREAM_AVAILABLE       = 18, ///< Could not find any suitable stream to play
		SP_ERROR_PERMISSION_DENIED         = 19, ///< Requested operation is not allowed
		SP_ERROR_INBOX_IS_FULL             = 20, ///< Target inbox is full
		SP_ERROR_NO_CACHE                  = 21, ///< Cache is not enabled
		SP_ERROR_NO_SUCH_USER              = 22, ///< Requested user does not exist
		SP_ERROR_NO_CREDENTIALS            = 23, ///< No credentials are stored
		SP_ERROR_NETWORK_DISABLED          = 24, ///< Network disabled
		SP_ERROR_INVALID_DEVICE_ID         = 25, ///< Invalid device ID
		SP_ERROR_CANT_OPEN_TRACE_FILE      = 26, ///< Unable to open trace file
		SP_ERROR_APPLICATION_BANNED        = 27, ///< This application is no longer allowed to use the Spotify service
		SP_ERROR_OFFLINE_TOO_MANY_TRACKS   = 31, ///< Reached the device limit for number of tracks to download
		SP_ERROR_OFFLINE_DISK_CACHE        = 32, ///< Disk cache is full so no more tracks can be downloaded to offline mode
		SP_ERROR_OFFLINE_EXPIRED           = 33, ///< Offline key has expired, the user needs to go online again
		SP_ERROR_OFFLINE_NOT_ALLOWED       = 34, ///< This user is not allowed to use offline mode
		SP_ERROR_OFFLINE_LICENSE_LOST      = 35, ///< The license for this device has been lost. Most likely because the user used offline on three other device
		SP_ERROR_OFFLINE_LICENSE_ERROR     = 36, ///< The Spotify license server does not respond correctly
		SP_ERROR_LASTFM_AUTH_ERROR         = 39, ///< A LastFM scrobble authentication error has occurred
		SP_ERROR_INVALID_ARGUMENT          = 40, ///< An invalid argument was specified
		SP_ERROR_SYSTEM_FAILURE            = 41, ///< An operating system error
	}

	/// <summary>
    /// The object type
    /// </summary>
	public enum SPItemType
    {
        Album,
        Artist,
        Playlist,
        Track
    }

	/// <summary>
	/// Album types.
	/// </summary>
	public enum SPAlbumType
    {
		[EnumMember(Value = "album")]
		Album       = 0, ///< Normal album
		[EnumMember(Value = "single")]
		Single      = 1, ///< Single
		[EnumMember(Value = "compilation")]
		Compilation = 2, ///< Compilation
		[EnumMember(Value = "unknown")]
		Unknown     = 3, ///< Unknown type
	}

	/// <summary>
	/// Track availability.
	/// </summary>
	public enum SPTrackAvailability
    {
		[EnumMember(Value = "unavailable")]
		Unavailable 		= 0, ///< Track is not available
		[EnumMember(Value = "available")]
		Available   		= 1, ///< Track is available and can be played
		[EnumMember(Value = "not_streamable")]
		NotStreamable 	= 2, ///< Track can not be streamed using this account
		[EnumMember(Value = "banned_by_artist")]
		BannedByArtist 	= 3, ///< Track not available on artist's reqeust
	}
    
    /// <summary>
	/// Track offline status.
	/// </summary>
	public enum SPTrackOfflineStatus
    {
		SP_TRACK_OFFLINE_NO             = 0, ///< Not marked for offline
		SP_TRACK_OFFLINE_WAITING        = 1, ///< Waiting for download
		SP_TRACK_OFFLINE_DOWNLOADING    = 2, ///< Currently downloading
		SP_TRACK_OFFLINE_DONE           = 3, ///< Downloaded OK and can be played
		SP_TRACK_OFFLINE_ERROR          = 4, ///< Error during download
		SP_TRACK_OFFLINE_DONE_EXPIRED   = 5, ///< Downloaded OK but not playable due to expiery
		SP_TRACK_OFFLINE_LIMIT_EXCEEDED = 6, ///< Waiting because device have reached max number of allowed tracks
		SP_TRACK_OFFLINE_DONE_RESYNC    = 7, ///< Downloaded OK and available but scheduled for re-download
	}
}