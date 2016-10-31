 /*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */


namespace Tizen.Network.IoTConnectivity
{
    /// <summary>
    /// This class represents remote response.
    /// It represents the response of all CRUD operations.
    /// </summary>
    public class RemoteResponse
    {
        internal RemoteResponse() { }

        /// <summary>
        /// Indicates the result of the response
        /// </summary>
        public ResponseCode Result { get; internal set; }

        /// <summary>
        /// Indicates representation of the response
        /// </summary>
        public Representation Representation { get; internal set; }

        /// <summary>
        /// Indicates header options of the response
        /// </summary>
        public ResourceOptions Options { get; internal set; }
    }
}
