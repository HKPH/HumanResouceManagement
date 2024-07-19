import React, { useEffect, useState } from 'react';
import { Layout, Menu } from 'antd';
import { UserOutlined, TeamOutlined } from '@ant-design/icons';
import { getAccounts } from '../api/accountApi'; 

const { Header, Content, Footer, Sider } = Layout;

interface AccountDto {
  id: number;
  username?: string;
  password?: string;
  role?: string;
  email?: string;
  employeeId?: number;
}

const MainLayout: React.FC = () => {
  const [accounts, setAccounts] = useState<AccountDto[]>([]);

  useEffect(() => {
    const fetchAccounts = async () => {
      const response = await getAccounts();
      setAccounts(response);
    };
    fetchAccounts();
  }, []);

  return (
    <Layout>
      <Sider>
        <Menu theme="dark" mode="inline">
          <Menu.SubMenu key="sub1" icon={<UserOutlined />} title="User">
            {accounts.map(account => (
              <Menu.Item key={account.id}>{account.username}</Menu.Item>
            ))}
          </Menu.SubMenu>
          <Menu.SubMenu key="sub2" icon={<TeamOutlined />} title="Team">
            {/* ... */}
          </Menu.SubMenu>
        </Menu>
      </Sider>
      <Layout>
        <Header>Header</Header>
        <Content>Content</Content>
        <Footer>Footer</Footer>
      </Layout>
    </Layout>
  );
};

export default MainLayout;
