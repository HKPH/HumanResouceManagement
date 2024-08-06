import React, { useState } from 'react';
import logo from '../assets/images/logo.png';
import defaultImage from '../assets/images/home.png';
import {
  MenuFoldOutlined,
  MenuUnfoldOutlined,
  AppstoreAddOutlined,
  AuditOutlined,
  SettingOutlined,
  BarChartOutlined,
} from '@ant-design/icons';
import { Button, Layout, Menu, theme } from 'antd';
import { useNavigate } from 'react-router-dom';
import BankList from '../components/BankList';
import DepartmentList from '../components/DepartmentList';
import BirthdayNotifications from '../components/Notifications';
import FileManagement from '../components/FileManagement';
import ContractDashboard from '../components/ContractDashboard';
import RewardDisciplineChart from '../components/RewardDisciplineChart';
import EmployeeContractReport from '../components/EmployeeContractReport';
import AssetReport from '../components/AssetChart';

const { Header, Sider, Content } = Layout;
const { SubMenu } = Menu;

const HomePage: React.FC = () => {
  const [collapsed, setCollapsed] = useState(false);
  const [selectedMenu, setSelectedMenu] = useState<string | null>(null);
  const navigate = useNavigate();
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  const handleLogout = () => {
    navigate('/');
  };

  const handleMenuClick = (key: string) => {
    setSelectedMenu(key);
  };

  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Sider
        trigger={null}
        collapsible
        collapsed={collapsed}
        style={{
          height: '100vh',
          position: 'fixed',
          left: 0,
          top: 0,
          overflowY: 'auto',
          overflowX: 'visible',
          zIndex: 10,
        }}
      >
        <div className="logo-container" style={{
          height: 64,
          backgroundColor: '#fff',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          padding: collapsed ? '0 16px' : '0 24px',
          borderBottom: '1px solid #e8e8e8'
        }}>
          <img
            src={logo}
            alt="Logo"
            style={{
              height: '100%',
              transition: 'height 0.3s'
            }}
          />
        </div>
        <Menu
          theme="dark"
          mode="inline"
          defaultSelectedKeys={['1']}
          onClick={(e) => handleMenuClick(e.key)}
          style={{ zIndex: 20 }}
        >
          <SubMenu
            key="1"
            icon={<AppstoreAddOutlined />}
            title="Danh mục"
          >
            <Menu.Item key="1-1">Chức danh</Menu.Item>
            <Menu.Item key="1-2">Hợp đồng</Menu.Item>
            <Menu.Item key="1-3">Phụ cấp</Menu.Item>
            <Menu.Item key="1-4">Phúc lợi</Menu.Item>
            <Menu.Item key="1-5">Tỉnh thành</Menu.Item>
            <Menu.Item key="1-6">Tài sản cấp phát</Menu.Item>
            <Menu.Item key="1-7">Ngân hàng</Menu.Item>
            <Menu.Item key="1-8">Chi nhánh ngân hàng</Menu.Item>
            <Menu.Item key="1-9">Nơi khám chữa bệnh</Menu.Item>
            <Menu.Item key="1-10">Khác</Menu.Item>
          </SubMenu>
          <SubMenu
            key="2"
            icon={<AuditOutlined />}
            title="Nghiệp vụ"
          >
            <Menu.Item key="2-1">Hồ sở nhân sự</Menu.Item>
            <Menu.Item key="2-2">Thông tin nhân sự</Menu.Item>
            <Menu.Item key="2-3">Quyết định điều chuyển</Menu.Item>
            <Menu.Item key="2-4">Hợp đồng lao động</Menu.Item>
            <Menu.Item key="2-5">Quản lý phụ cấp</Menu.Item>
            <Menu.Item key="2-6">Quản lý phúc lợi</Menu.Item>
            <Menu.Item key="2-7">Quản lý tài sản cấp phát</Menu.Item>
            <Menu.Item key="2-8">Quản lý kỷ luật</Menu.Item>
            <Menu.Item key="2-9">Quản lý khen thưởng</Menu.Item>
            <Menu.Item key="2-10">Quản lý nghỉ việc</Menu.Item>
            <Menu.Item key="2-11">Quản lý tệp tin đính kèm</Menu.Item>
          </SubMenu>
          <SubMenu
            key="3"
            icon={<SettingOutlined />}
            title="Thiết lập"
          >
            <Menu.Item key="3-1">Phụ cấp</Menu.Item>
            <Menu.Item key="3-2">Phúc lợi</Menu.Item>
            <Menu.Item key="3-3">Sơ đồ tổ chức</Menu.Item>
            <Menu.Item key="3-4">Chức danh theo đơn vị</Menu.Item>
          </SubMenu>
          <SubMenu
            key="4"
            icon={<BarChartOutlined />}
            title="Báo cáo"
          >
            <Menu.Item key="4-1">Hợp đồng lao động</Menu.Item>
            <Menu.Item key="4-2">Kỷ luật, khen thưởng</Menu.Item>
            <Menu.Item key="4-3">Tỉ lệ nhân viên</Menu.Item>
            <Menu.Item key="4-4">Tài sản cấp phát</Menu.Item>

          </SubMenu>
        </Menu>
      </Sider>
      <Layout style={{ marginLeft: collapsed ? 80 : 200 }}>
        <Header style={{ padding: 0, background: colorBgContainer, height: 64 }}>
          <Button
            type="text"
            icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
            onClick={() => setCollapsed(!collapsed)}
            style={{
              fontSize: '16px',
              width: 64,
              height: 64,
            }}
          />
          <BirthdayNotifications />
          <Button
            type="text"
            onClick={handleLogout}
            style={{
              position: 'absolute',
              right: 0,
              top: 0,
              margin: '16px',
            }}
          >
            Logout
          </Button>
        </Header>
        <Content style={{ margin: '24px 16px', padding: 24, background: colorBgContainer, minHeight: 280, borderRadius: borderRadiusLG }}>
          {selectedMenu === null && (
            <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100%' }}>
              <img src={defaultImage} alt="Default" style={{ maxWidth: '100%', maxHeight: '100%' }} />
            </div>
          )}
          {selectedMenu === '1-7' && <BankList />}
          {selectedMenu === '3-4' && <DepartmentList />}
          {selectedMenu === '2-11' && <FileManagement />}
          {selectedMenu === '4-1' && <ContractDashboard />}
          {selectedMenu === '4-2' && <RewardDisciplineChart />}
          {selectedMenu === '4-3' && <EmployeeContractReport />}
          {selectedMenu === '4-4' && <AssetReport />}

        </Content>
      </Layout>
    </Layout>
  );
};

export default HomePage;
