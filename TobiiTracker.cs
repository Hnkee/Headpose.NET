using Headpose.NET.Tobii.Tobii.StreamEngine;
using System;
using System.Linq;
using System.Threading;

namespace Headpose.NET
{

    internal class TobiiTracker : IDisposable
    {

        public float PositionX { get; private set; }
        public float PositionY { get; private set; }
        public float PositionZ { get; private set; }

        public float RotationX { get; private set; }
        public float RotationY { get; private set; }
        public float RotationZ { get; private set; }

        protected DeviceContext _device;

        private readonly Thread _thread;

        private bool _continue = true;
        private tobii_head_pose_callback_t _headPoseCallbackInstance;

        internal TobiiTracker(ApiContext api)
        {
            _device = api
                .GetDeviceUrls()
                .Select(u => new DeviceContext(api.Handle, u))
                .FirstOrDefault();

            if (_device != null)
            {
                var result = Interop.tobii_stream_supported(_device.Handle, tobii_stream_t.TOBII_STREAM_HEAD_POSE, out var headPoseSupported);
                if (result == tobii_error_t.TOBII_ERROR_NO_ERROR && headPoseSupported)
                {
                    _headPoseCallbackInstance = HeadPoseCallback;
                    result = Interop.tobii_head_pose_subscribe(_device.Handle, _headPoseCallbackInstance);
                }

                _thread = new Thread(DataPump);
                _thread.IsBackground = true;
                _thread.Name = $"Headpose datapump for {_device.SerialNumber}";
                _thread.Start();
            }
        }


        public void Dispose()
        {
            _continue = false;
            if (_headPoseCallbackInstance != null) Interop.tobii_head_pose_unsubscribe(_device.Handle);

            _device.Dispose();
            _thread.Join(300);
            _headPoseCallbackInstance = null;
        }


        protected virtual void HeadPoseCallback(ref tobii_head_pose_t data)
        {
            if (data.position_validity == tobii_validity_t.TOBII_VALIDITY_VALID)
            {
                PositionX = data.position_xyz.x;
                PositionY = data.position_xyz.y;
                PositionZ = data.position_xyz.z;
            }

            if (data.rotation_x_validity == tobii_validity_t.TOBII_VALIDITY_VALID) RotationX = data.rotation_xyz.x;
            if (data.rotation_y_validity == tobii_validity_t.TOBII_VALIDITY_VALID) RotationY = data.rotation_xyz.y;
            if (data.rotation_z_validity == tobii_validity_t.TOBII_VALIDITY_VALID) RotationZ = data.rotation_xyz.z;

        }

        private void DataPump()
        {
            while (_continue)
            {
                _device.Pump();
            }
        }
    }

}
