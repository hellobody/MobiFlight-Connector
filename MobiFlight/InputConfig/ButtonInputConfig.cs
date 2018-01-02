﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MobiFlight.InputConfig
{
    public enum ButtonInputEventType
    {
        PRESS,
        RELEASE,
        REPEAT
    }

    public class ButtonInputConfig : IXmlSerializable, ICloneable
    {
        public InputAction onPress;
        public InputAction onRelease;


        public object Clone()
        {
            ButtonInputConfig clone = new ButtonInputConfig();
            if (onPress != null) clone.onPress = (InputAction) onPress.Clone();
            if (onRelease != null) clone.onRelease = (InputAction) onRelease.Clone();
            return clone;
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return (null);
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {

            reader.Read(); // this should be the opening tag "onPress"
            if (reader.LocalName == "") reader.Read();
            if (reader.LocalName == "onPress")
            {
                switch (reader["type"])
                {
                    case FsuipcOffsetInputAction.TYPE:
                        onPress = new FsuipcOffsetInputAction();
                        onPress.ReadXml(reader);
                        reader.Read(); // this should be the closing tag "onPress"
                        break;

                    case KeyInputAction.TYPE:
                        onPress = new KeyInputAction();
                        onPress.ReadXml(reader);
                        break;

                    case EventIdInputAction.TYPE:
                        onPress = new EventIdInputAction();
                        onPress.ReadXml(reader);
                        break;

                    case JeehellInputAction.TYPE:
                        onPress = new JeehellInputAction();
                        onPress.ReadXml(reader);
                        break;

                    case LuaMacroInputAction.TYPE:
                        onPress = new LuaMacroInputAction();
                        onPress.ReadXml(reader);
                        break;

                    case RetriggerInputAction.TYPE:
                        onPress = new RetriggerInputAction();
                        onPress.ReadXml(reader);
                        break;

                    case VJoyInputAction.TYPE:
                        onPress = new VJoyInputAction();
                        onPress.ReadXml(reader);
                        break;
                }
            }

            reader.Read();
            if (reader.LocalName == "") reader.Read();
            if (reader.LocalName == "onRelease")
            {
                switch (reader["type"])
                {
                    case FsuipcOffsetInputAction.TYPE:
                        onRelease = new FsuipcOffsetInputAction();
                        onRelease.ReadXml(reader);
                        reader.ReadStartElement();
                        break;

                    case KeyInputAction.TYPE:
                        onRelease = new KeyInputAction();
                        onRelease.ReadXml(reader);
                        reader.ReadStartElement();
                        break;

                    case EventIdInputAction.TYPE:
                        onRelease = new EventIdInputAction();
                        onRelease.ReadXml(reader);
                        reader.ReadStartElement();
                        break;

                    case JeehellInputAction.TYPE:
                        onRelease = new JeehellInputAction();
                        onRelease.ReadXml(reader);
                        break;

                    case LuaMacroInputAction.TYPE:
                        onRelease = new LuaMacroInputAction();
                        onRelease.ReadXml(reader);
                        break;

                    case RetriggerInputAction.TYPE:
                        onRelease = new RetriggerInputAction();
                        onRelease.ReadXml(reader);
                        break;

                    case VJoyInputAction.TYPE:
                        onRelease = new VJoyInputAction();
                        onRelease.ReadXml(reader);
                        break;
                }
            }
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteStartElement("onPress");
            if (onPress != null) onPress.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("onRelease");
            if (onRelease != null) onRelease.WriteXml(writer);
            writer.WriteEndElement();
        }

        internal void execute(FSUIPC.FSUIPCCacheInterface fsuipcCache, MobiFlightCache moduleCache, ButtonArgs e)
        {
            if (e.Value == 0 && onPress != null)
            {
                Log.Instance.log("Executing OnPress: " + e.ButtonId + "@" + e.Serial, LogSeverity.Debug);
                onPress.execute(fsuipcCache, moduleCache);
                }
            else if (e.Value == 1 && onPress != null)
            {
                Log.Instance.log("Executing OnRelease: " + e.ButtonId + "@" + e.Serial, LogSeverity.Debug);
                onRelease.execute(fsuipcCache, moduleCache);
            }

        }
    }

}
