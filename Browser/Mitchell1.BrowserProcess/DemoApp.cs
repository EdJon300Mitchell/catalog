using System;
using System.Diagnostics;
using System.Threading;
using Xilium.CefGlue;

namespace Mitchell1.BrowserProcessAny
{
	internal sealed class DemoApp : CefApp
	{
	    private CefRenderProcessHandler renderProcessHandler = new DemoRenderProcessHandler();

	    protected override CefRenderProcessHandler GetRenderProcessHandler()
	    {
	        return renderProcessHandler;
	    }
	}

    internal class DemoRenderProcessHandler : CefRenderProcessHandler
    {
        MyCustomCefV8Handler myCefV8Handler = new MyCustomCefV8Handler();

        protected override void OnWebKitInitialized()
        {
            base.OnWebKitInitialized();
            var nativeFunction = @"
            var nativeImplementation;
            if(!nativeImplementation)
                nativeImplementation = {};

            nativeImplementation = function(action /*, ...*/) {
                native function MyNativeFunction(arguments);
                return MyNativeFunction.apply(this, arguments);
            };";

            CefRuntime.RegisterExtension("myExtension", nativeFunction, myCefV8Handler);
        }
    }

    internal class MyCustomCefV8Handler : CefV8Handler
    {
        protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {
#if DEBUG
            Trace.WriteLine(String.Format("{0} JS Callback Execute: name-{1}, thread-{2}, args-{3}", Process.GetCurrentProcess().Id, name, Thread.CurrentThread.Name, arguments != null ? arguments.Length : 0));

            if (arguments != null)
            {
                int i = 0;
                foreach (var a in arguments)
                {
                    Trace.WriteLine(String.Format("Arg-{0}: {1}", i++, a));
                }
            }
#endif

            var message = CefProcessMessage.Create("Extension-" + name);
            var args = message.Arguments;
            CefV8Array2ListValue(arguments, ref args);
           
            CefV8Context.GetCurrentContext().GetFrame().SendProcessMessage(CefProcessId.Browser, message);

            returnValue = CefV8Value.CreateBool(true);
            exception = null;
            return true;
        }

        void CefV8Array2ListValue(CefV8Value[] source, ref CefListValue target) 
        {
            if (source == null || source.Length == 0)
            {
                return;
            }

            int arg_length = source.Length;
            if (arg_length == 0)
                return;

            // Fill with nulls
            target.SetSize(arg_length);
   
            for (int i = 0; i < arg_length; ++i)
            {
                var value = source[i];
                if (value.IsBool)
                {
                    target.SetBool(i, value.GetBoolValue());
                } 
                else if (value.IsInt||value.IsUInt) 
                {
                    target.SetInt(i, value.GetIntValue());
                }
                else if (value.IsDouble) 
                {
                    target.SetDouble(i, value.GetDoubleValue());
                }
                else if (value.IsNull)
                {
                    target.SetNull(i);
                }
                else if (value.IsString)
                {
                    target.SetString(i, value.GetStringValue());
                } 
                else if (value.IsDate)
                {
                    var new_dictionary = CefDictionaryValue.Create();
                    new_dictionary.SetString("type", "datetime");
                    new_dictionary.SetBinary("value", CefBinaryValue.Create(BitConverter.GetBytes(value.GetDateValue().ToBinary())));
                    target.SetDictionary(i, new_dictionary);
                }
                else if (value.IsArray)
                {
                    var new_list = CefListValue.Create();
                    CefV8Array2ListValue(ConvertToArray(value), ref new_list);
                    target.SetList(i, new_list);
                }
                else if (value.IsObject)
                {
                    CefDictionaryValue new_dictionary = CefDictionaryValue.Create();
                    new_dictionary.SetString("type", "object");
                    CefDictionaryValue object_dictionary = CefDictionaryValue.Create();
                    CefV8JsonObject2DictionaryValue(value, ref object_dictionary);
                    new_dictionary.SetDictionary("value", object_dictionary);

                    target.SetDictionary(i, new_dictionary);
                }
            }
        }

        internal static CefV8Value[] ConvertToArray(CefV8Value valueArray)
        {
            if (!valueArray.IsArray && valueArray.GetArrayLength() <= 0)
            {
                return new CefV8Value[0];
            }

            var retArray = new CefV8Value[valueArray.GetArrayLength()];
            for (int i = 0; i < valueArray.GetArrayLength(); ++i)
            {
                retArray[i] = valueArray.GetValue(i);
            }

            return retArray;
        }

        void CefV8JsonObject2DictionaryValue(CefV8Value source, ref CefDictionaryValue target)
        {
            if (!source.IsObject)
            {
                return;
            }

            string [] keys = source.GetKeys();
            foreach(var key in keys)
            {
                CefV8Value value = source.GetValue(key);
      
                if (value.IsBool)
                {
                    target.SetBool(key, value.GetBoolValue());
                }
                else if (value.IsDouble)
                {
                    target.SetDouble(key, value.GetDoubleValue());
                }
                else if (value.IsInt || value.IsUInt)
                {
                    target.SetInt(key, value.GetIntValue());
                }
                else if (value.IsNull)
                {
                    target.SetNull(key);
                }
                else if (value.IsString)
                {
                    target.SetString(key,value.GetStringValue());
                }
                else if (value.IsDate)
                {
                    var new_dictionary = CefDictionaryValue.Create();
                    new_dictionary.SetString("type", "datetime");
                    new_dictionary.SetBinary("value", CefBinaryValue.Create(BitConverter.GetBytes(value.GetDateValue().ToBinary())));
                    target.SetDictionary(key, new_dictionary);
                }
                else if (value.IsArray)
                {
                    var new_list = CefListValue.Create();
                    CefV8Array2ListValue(ConvertToArray(value), ref new_list);
                    target.SetList(key, new_list);
                }
                else if (value.IsObject)
                {
                    CefDictionaryValue new_dictionary = CefDictionaryValue.Create();
                    new_dictionary.SetString("type", "object");
                    CefDictionaryValue object_dictionary = CefDictionaryValue.Create();
                    CefV8JsonObject2DictionaryValue(value, ref object_dictionary);
                    new_dictionary.SetDictionary("value", object_dictionary);

                    target.SetDictionary(key, new_dictionary);
                }
                else
                {
                    target.SetNull(key);
                }
            }
        }
    }
}
