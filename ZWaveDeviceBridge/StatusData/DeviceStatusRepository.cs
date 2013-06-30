using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate;

namespace ZWaveDeviceBridge.StatusData
{
    public class DeviceStatusRepository : IDeviceStatusRepository
    {
        private static DeviceStatusRepository s_theRepository;

        public static IDeviceStatusRepository TheRepository
        {
            get
            {
                if (null == s_theRepository)
                {
                    s_theRepository = new DeviceStatusRepository();
                }
                return s_theRepository;
            }
        }

        private ISessionFactory m_sessionFactory;
        private Configuration m_configuration;

        private DeviceStatusRepository()
        {
            m_configuration = new Configuration();
            m_configuration.Configure();
            m_configuration.AddAssembly(typeof(DeviceStatus ).Assembly);
            m_sessionFactory = m_configuration.BuildSessionFactory();
        }

        public void Add(DeviceStatus status)
        {
            using (ISession session = m_sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(status);
                    transaction.Commit();
                }
            }
        }

        public DeviceStatus GetStatus(Guid statusId)
        {
            using (ISession session = m_sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    return session.Get<DeviceStatus>(statusId);
                }
            }
        }

        public DeviceStatus GetLatestStatus(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<DeviceStatus> GetStatus(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<DeviceStatus> GetStatus(DateTime beging, DateTime ending)
        {
            throw new NotImplementedException();
        }
    }
}
