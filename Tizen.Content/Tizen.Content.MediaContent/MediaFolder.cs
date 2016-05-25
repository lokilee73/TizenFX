﻿/// Copyright 2016 by Samsung Electronics, Inc.,
///
/// This software is the confidential and proprietary information
/// of Samsung Electronics, Inc.("Confidential Information"). You
/// shall not disclose such Confidential Information and shall use
/// it only in accordance with the terms of the license agreement
/// you entered into with Samsung.


using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tizen.Content.MediaContent
{
    /// <summary>
    /// A Folder is used to organize media content files i.e. image, audio, video files, in the physical storage of the device.
    /// The Media Folder API provides functions to get basic information about existing folders e.g. folder name, path and storage type.
    /// It also provides functions to get information related to media items present in the folder.
    /// </summary>
    public class MediaFolder : ContentCollection
    {
        private IntPtr _folderHandle;
        private bool _disposedValue = false;
        internal IntPtr Handle
        {
            get
            {
                return _folderHandle;
            }
            set
            {
                _folderHandle = value;
            }
        }
        /// <summary>
        /// The ID of the media folder
        /// </summary>
        public string Id
        {
            get
            {
                string id;
                MediaContentError res = (MediaContentError)Interop.Folder.GetFolderId(_folderHandle, out id);
                if (res != MediaContentError.None)
                {
                    Log.Warn(MediaContentErrorFactory.LogTag, "Failed to get Id for the MediaFolder");
                }
                return id;
            }
        }

        /// <summary>
        /// The ID of the upper media folder
        /// </summary>
        public string ParentId
        {
            get
            {
                string parentId;
                MediaContentError res = (MediaContentError)Interop.Folder.GetParentFolderId(_folderHandle, out parentId);
                if (res != MediaContentError.None)
                {
                    Log.Warn(MediaContentErrorFactory.LogTag, "Failed to get parents folder Id for the MediaFolder");
                }
                return parentId;
            }
        }

        /// <summary>
        /// The path of the media folder
        /// </summary>
        public string FolderPath
        {
            get
            {
                string path;
                MediaContentError res = (MediaContentError)Interop.Folder.GetPath(_folderHandle, out path);
                if (res != MediaContentError.None)
                {
                    Log.Warn(MediaContentErrorFactory.LogTag, "Failed to get path for the MediaFolder");
                }
                return path;
            }
        }

        /// <summary>
        /// The name of the media folder
        /// </summary>
        public string Name
        {
            get
            {
                string name;
                MediaContentError res = (MediaContentError)Interop.Folder.GetName(_folderHandle, out name);
                if (res != MediaContentError.None)
                {
                    Log.Warn(MediaContentErrorFactory.LogTag, "Failed to get name for the MediaFolder");
                }
                return name;
            }
            set
            {
                MediaContentError res = (MediaContentError)Interop.Folder.SetName(_folderHandle, value);
                if (res != MediaContentError.None)
                {
                    Log.Warn(MediaContentErrorFactory.LogTag, "Failed to set name for the MediaFolder");
                }
            }
        }

        /// <summary>
        /// The storage type of the media folder
        /// </summary>
        public ContentStorageType StorageType
        {
            get
            {
                int type;
                MediaContentError res = (MediaContentError)Interop.Folder.GetStorageType(_folderHandle, out type);
                if (res != MediaContentError.None)
                {
                    Log.Warn(MediaContentErrorFactory.LogTag, "Failed to get storage type for the MediaFolder");
                }
                return (ContentStorageType)type;
            }
        }

        /// <summary>
        /// The storage id of the media folder
        /// </summary>
        public string StorageId
        {
            get
            {
                string storageId;
                MediaContentError res = (MediaContentError)Interop.Folder.GetStorageId(_folderHandle, out storageId);
                if (res != MediaContentError.None)
                {
                    Log.Warn(MediaContentErrorFactory.LogTag, "Failed to get storage Id for the MediaFolder");
                }
                return storageId;
            }
        }

        /// <summary>
        /// The modified date of the media folder
        /// </summary>
        public DateTime ModifiedTime
        {
            get
            {
                DateTime date;
                MediaContentError res = (MediaContentError)Interop.Folder.GetModifiedTime(_folderHandle, out date);
                if (res != MediaContentError.None)
                {
                    Log.Warn(MediaContentErrorFactory.LogTag, "Failed to get modified date for the MediaFolder");
                }
                return date;
            }
        }

        /// <summary>
        /// The folder order info. Get/Set the folder viewing order
        /// </summary>
        public ContentOrder Order
        {
            get
            {
                int order;
                MediaContentError res = (MediaContentError)Interop.Folder.GetOrder(_folderHandle, out order);
                if (res != MediaContentError.None)
                {
                    Log.Warn(MediaContentErrorFactory.LogTag, "Failed to get modified date for the MediaFolder");
                }
                return (ContentOrder)order;
            }
            set
            {
                MediaContentError res = (MediaContentError)Interop.Folder.SetOrder(_folderHandle, (int)value);
                if (res != MediaContentError.None)
                {
                    Log.Warn(MediaContentErrorFactory.LogTag, "Failed to set viewing order for the MediaFolder");
                }
            }
        }

        internal MediaFolder(IntPtr _folderHandle)
        {
            this.Handle = _folderHandle;
        }

        /// <summary>
        /// Gets the count of media files for the passed filter in the given folder from the media database.
        /// </summary>
        /// <param name="filter">ContentFilter used to match media content from teh media database.</param>
        /// <returns>The number of media contents matching the filter passed</returns>
        public override int GetMediaInformationCount(ContentFilter filter)
        {
            int mediaCount;
            IntPtr handle = (filter != null) ? filter.Handle : IntPtr.Zero;
            MediaContentError res = (MediaContentError)Interop.Folder.GetMediaCountFromDb(Id, handle, out mediaCount);
            if (res != MediaContentError.None)
            {
                Log.Warn(MediaContentErrorFactory.LogTag, "Failed to get media count for the folder");
            }
            return mediaCount;
        }

        ~MediaFolder()
        {
            Dispose(false);
        }
       public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (_folderHandle != IntPtr.Zero)
                {
                    Console.WriteLine("Before destroy");
                    Interop.Folder.Destroy(_folderHandle);
                    _folderHandle = IntPtr.Zero;
                    Console.WriteLine("After destroy");
                }
                _disposedValue = true;
            }
        }
        /// <summary>
        /// Iterates through the media files with an optional filter in the given folder from the media database.
        /// This function gets all media files associated with the given folder and meeting desired filter option.
        /// If NULL is passed to the filter, no filtering is applied.
        /// </summary>
        /// <param name="filter">ContentFilter used to match media content from the media database.</param>
        /// <returns>List of content media items matching the passed filter</returns>
        public override Task<IEnumerable<MediaInformation>> GetMediaInformationsAsync(ContentFilter filter)
        {
            var tcs = new TaskCompletionSource<IEnumerable<MediaInformation>>();
            List<MediaInformation> mediaContents = new List<MediaInformation>();
            IntPtr handle = (filter != null) ? filter.Handle : IntPtr.Zero;
            MediaContentError res;
            Interop.Folder.MediaInfoCallback callback = (Interop.MediaInformation.SafeMediaInformationHandle mediaHandle, IntPtr data) =>
            {
                //TODO: Create MediaInformation using safehandle.
                Interop.MediaInformation.SafeMediaInformationHandle newHandle;
                res = (MediaContentError)Interop.MediaInformation.Clone(out newHandle, mediaHandle.DangerousGetHandle());
                if (res != MediaContentError.None)
                {
                    Log.Warn(MediaContentErrorFactory.LogTag, "Failed to clone media information");
                }
                MediaInformation info = new MediaInformation(newHandle);
                mediaContents.Add(info);
            };
            res = (MediaContentError)Interop.Folder.ForeachMediaFromDb(Id, handle, callback, IntPtr.Zero);
            if (res != MediaContentError.None)
            {
                Log.Warn(MediaContentErrorFactory.LogTag, "Failed to get media information for the folder");
            }
            tcs.TrySetResult(mediaContents);
            return tcs.Task;
        }
    }
}
