using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerServer
{
    public class MouseSignal
    {
        public const string DRAG = "DRAG";
        public const string SCROLL = "SCROLL";
        public const string TAP = "TAP";
        public const string DOUBLE_TAP = "DOUBLE_TAP";
        public const string HOLD = "HOLD";
        public const string END_SIMULATION = "END_SIMULATION";

        private double _xCoordinate;
        public double XCoordinate
        {
            get
            {
                return _xCoordinate;
            }
            set
            {
                if (value != _xCoordinate)
                {
                    _xCoordinate = value;
                }
            }
        }

        private double _yCoordinate;
        public double YCoordinate
        {
            get
            {
                return _yCoordinate;
            }
            set
            {
                if (value != _yCoordinate)
                {
                    _yCoordinate = value;
                }
            }
        }

        private string _action;
        public string Action
        {
            get
            {
                return _action;
            }
            set
            {
                if (value != _action)
                {
                    _action = value;
                }
            }
        }

        /*public MouseSignal(double x, double y, string action)
        {
            _xCordinate = x;
            _yCordinate = y;
            _action = action;
        }

        public MouseSignal(string action)
        {
            _xCordinate = 20;
            _yCordinate = 20;
            _action = action;
        }

        public MouseSignal()
        {
            _xCordinate = 10;
            _yCordinate = 10;
            _action = "";
        }*/
    }
}
