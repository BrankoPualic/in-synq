﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InSynq.Common.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ResourceValidation {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResourceValidation() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("InSynq.Common.Resources.ResourceValidation", typeof(ResourceValidation).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} with this {1} already exist..
        /// </summary>
        public static string Already_Exist {
            get {
                return ResourceManager.GetString("Already_Exist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} and {1} fields don&apos;t match..
        /// </summary>
        public static string Dont_Match {
            get {
                return ResourceManager.GetString("Dont_Match", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} size is too large. Maximum allowed size is {1}..
        /// </summary>
        public static string File_Too_Large {
            get {
                return ResourceManager.GetString("File_Too_Large", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is in the wrong format. Acceptable formats: {1}..
        /// </summary>
        public static string File_Wrong_Format {
            get {
                return ResourceManager.GetString("File_Wrong_Format", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is invalid..
        /// </summary>
        public static string Invalid {
            get {
                return ResourceManager.GetString("Invalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your credentials are incorrect. Please try again..
        /// </summary>
        public static string Invalid_Credentials {
            get {
                return ResourceManager.GetString("Invalid_Credentials", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} can&apos;t be more than {1} characters long..
        /// </summary>
        public static string MaximumLength {
            get {
                return ResourceManager.GetString("MaximumLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} must be at least {1} characters long..
        /// </summary>
        public static string MinimumLength {
            get {
                return ResourceManager.GetString("MinimumLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is required..
        /// </summary>
        public static string Required {
            get {
                return ResourceManager.GetString("Required", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is in the wrong format..
        /// </summary>
        public static string Wrong_Format {
            get {
                return ResourceManager.GetString("Wrong_Format", resourceCulture);
            }
        }
    }
}
