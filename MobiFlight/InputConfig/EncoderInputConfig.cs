﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MobiFlight.InputConfig 
{
    public enum EncoderInputDirectionType
    {
        LEFT,
        RIGHT
    }

    public enum EncoderInputEventType
    {
        NORMAL,
        FAST,
        FASTEST
    }

    public class EncoderInputConfig : IXmlSerializable, ICloneable
    {
        public InputAction onLeft;
        public InputAction onLeftFast;
        public InputAction onRight;
        public InputAction onRightFast;
        
        public object Clone()
        {
            EncoderInputConfig clone = new EncoderInputConfig();
            if (onLeft != null) clone.onLeft = (InputAction)onLeft.Clone();
            if (onLeftFast != null) clone.onLeftFast = (InputAction)onLeftFast.Clone();
            if (onRight != null) clone.onRight = (InputAction)onRight.Clone();
            if (onRightFast != null) clone.onRightFast = (InputAction)onRightFast.Clone();
            return clone;
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return (null);
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            reader.Read(); // this should be the opening tag "onPress"
            if (reader.LocalName == "onLeft")
            {
                switch (reader["type"])
                {
                    case FsuipcOffsetInputAction.TYPE:
                        onLeft = new FsuipcOffsetInputAction();
                        onLeft.ReadXml(reader);
                        reader.ReadStartElement(); // this should be the closing tag "onPress"
                        break;

                    case KeyInputAction.TYPE:
                        onLeft = new KeyInputAction();
                        onLeft.ReadXml(reader);
                        break;

                    case EventIdInputAction.TYPE:
                        onLeft = new EventIdInputAction();
                        onLeft.ReadXml(reader);
                        break;

                    case JeehellInputAction.TYPE:
                        onLeft = new JeehellInputAction();
                        onLeft.ReadXml(reader);
                        break;

                    case LuaMacroInputAction.TYPE:
                        onLeft = new LuaMacroInputAction();
                        onLeft.ReadXml(reader);
                        break;
                }
            }

            reader.Read(); // this should be the opening tag "onPress"
            if (reader.LocalName == "onLeftFast")
            {
                switch (reader["type"])
                {
                    case FsuipcOffsetInputAction.TYPE:
                        onLeftFast = new FsuipcOffsetInputAction();
                        onLeftFast.ReadXml(reader);
                        reader.ReadStartElement(); // this should be the closing tag "onPress"
                        break;

                    case KeyInputAction.TYPE:
                        onLeftFast = new KeyInputAction();
                        onLeftFast.ReadXml(reader);
                        break;

                    case EventIdInputAction.TYPE:
                        onLeftFast = new EventIdInputAction();
                        onLeftFast.ReadXml(reader);
                        break;

                    case JeehellInputAction.TYPE:
                        onLeftFast = new JeehellInputAction();
                        onLeftFast.ReadXml(reader);
                        break;

                    case LuaMacroInputAction.TYPE:
                        onLeftFast = new LuaMacroInputAction();
                        onLeftFast.ReadXml(reader);
                        break;
                }
            }

            reader.Read();
            if (reader.LocalName == "onRight")
            {
                switch (reader["type"])
                {
                    case FsuipcOffsetInputAction.TYPE:
                        onRight = new FsuipcOffsetInputAction();
                        onRight.ReadXml(reader);
                        reader.ReadStartElement();
                        break;

                    case KeyInputAction.TYPE:
                        onRight = new KeyInputAction();
                        onRight.ReadXml(reader);
                        break;

                    case EventIdInputAction.TYPE:
                        onRight = new EventIdInputAction();
                        onRight.ReadXml(reader);
                        break;

                    case JeehellInputAction.TYPE:
                        onRight = new JeehellInputAction();
                        onRight.ReadXml(reader);
                        break;

                    case LuaMacroInputAction.TYPE:
                        onRight = new LuaMacroInputAction();
                        onRight.ReadXml(reader);
                        break;
                }
            }

            reader.Read();
            if (reader.LocalName == "onRightFast")
            {
                switch (reader["type"])
                {
                    case FsuipcOffsetInputAction.TYPE:
                        onRightFast = new FsuipcOffsetInputAction();
                        onRightFast.ReadXml(reader);
                        reader.ReadStartElement();
                        break;

                    case KeyInputAction.TYPE:
                        onRightFast = new KeyInputAction();
                        onRightFast.ReadXml(reader);
                        break;

                    case EventIdInputAction.TYPE:
                        onRightFast = new EventIdInputAction();
                        onRightFast.ReadXml(reader);
                        break;

                    case JeehellInputAction.TYPE:
                        onRightFast = new JeehellInputAction();
                        onRightFast.ReadXml(reader);
                        break;

                    case LuaMacroInputAction.TYPE:
                        onRightFast = new LuaMacroInputAction();
                        onRightFast.ReadXml(reader);
                        break;
                }
            }
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteStartElement("onLeft");
            if (onLeft != null) onLeft.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("onLeftFast");
            if (onLeftFast != null) onLeftFast.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("onRight");
            if (onRight != null) onRight.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("onRightFast");
            if (onRightFast != null) onRightFast.WriteXml(writer);
            writer.WriteEndElement();
        }

        internal void execute(FSUIPC.FSUIPCCacheInterface fsuipcCache, MobiFlightCache moduleCache, ButtonArgs e)
        {
            if ((e.Value == 0 && onLeft != null) || (e.Value == 1 && onLeftFast == null))
            {
                Log.Instance.log("Executing OnLeft: " + e.ButtonId + "@" + e.Serial, LogSeverity.Debug);
                onLeft.execute(fsuipcCache, moduleCache);
            }
            else if (e.Value == 1 && onLeftFast != null)
            {
                Log.Instance.log("Executing OnLeftFast: " + e.ButtonId + "@" + e.Serial, LogSeverity.Debug);
                onLeftFast.execute(fsuipcCache, moduleCache);
            }
            else if ((e.Value == 2 && onRight != null) || (e.Value == 3 && onRightFast == null))
            {
                Log.Instance.log("Executing OnRight: " + e.ButtonId + "@" + e.Serial, LogSeverity.Debug);
                onRight.execute(fsuipcCache, moduleCache);
            }
            else if (e.Value == 3 && onRightFast != null)
            {
                Log.Instance.log("Executing OnRightFast: " + e.ButtonId + "@" + e.Serial, LogSeverity.Debug);
                onRightFast.execute(fsuipcCache, moduleCache);
            }

        }
    }
}
