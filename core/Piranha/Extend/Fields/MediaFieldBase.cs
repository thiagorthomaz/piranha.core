/*
 * Copyright (c) 2017 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * https://github.com/piranhacms/piranha.core
 * 
 */

using Newtonsoft.Json;
using System;

namespace Piranha.Extend.Fields
{
    public class MediaFieldBase<T> : IField, IEquatable<T> where T : MediaFieldBase<T>
    {
        /// <summary>
        /// Gets the list item title if this field is used in
        /// a collection regions.
        /// </summary>
        public virtual string GetTitle() {
            if (Media != null)
                return Media.Filename;
            return null;
        }

        /// <summary>
        /// Gets/sets the media id.
        /// </summary>
        /// <returns></returns>
        public Guid? Id { get; set; }

        /// <summary>
        /// Gets/sets the related media object.
        /// </summary>
        [JsonIgnore]
        public Data.Media Media { get; private set; }

        /// <summary>
        /// Gets if the field has a media object available.
        /// </summary>
        public bool HasValue {
            get { return Media != null; }
        }

        /// <summary>
        /// Initializes the field for client use.
        /// </summary>
        /// <param name="api">The current api</param>
        public virtual void Init(IApi api) { 
            if (Id.HasValue) {
                Media = api.Media.GetById(Id.Value);

                if (Media == null) {
                    // The image has been removed, remove the
                    // missing id.
                    Id = null;
                }
            }
        }

        /// <summary>
        /// Gets the hash code for the field.
        /// </summary>
        public override int GetHashCode() {
            return Id.HasValue ? Id.GetHashCode() : 0;
        }

        /// <summary>
        /// Checks if the given object is equal to the field.
        /// </summary>
        /// <param name="obj">The object</param>
        /// <returns>True if the fields are equal</returns>
        public override bool Equals(object obj) {
            if (obj is T)
                return Equals((T)obj);
            return false;
        }

        /// <summary>
        /// Checks if the given field is equal to the field.
        /// </summary>
        /// <param name="obj">The field</param>
        /// <returns>True if the fields are equal</returns>
        public virtual bool Equals(T obj) {
            return Id == obj.Id;
        }

        /// <summary>
        /// Checks if the fields are equal.
        /// </summary>
        /// <param name="field1">The first field</param>
        /// <param name="field2">The second field</param>
        /// <returns>True if the fields are equal</returns>
        public static bool operator ==(MediaFieldBase<T> field1, MediaFieldBase<T> field2) {
            return field1.Equals(field2);
        }

        /// <summary>
        /// Checks if the fields are not equal.
        /// </summary>
        /// <param name="field1">The first field</param>
        /// <param name="field2">The second field</param>
        /// <returns>True if the fields are equal</returns>
        public static bool operator !=(MediaFieldBase<T> field1, MediaFieldBase<T> field2) {
            return !field1.Equals(field2);
        }
    }
}
