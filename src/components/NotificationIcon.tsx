import React from 'react';
import { BellOutlined } from '@ant-design/icons';
import { Badge, Dropdown, Menu } from 'antd';

const NotificationIcon: React.FC = () => {
  const menu = (
    <Menu>
      <Menu.Item key="1">Notification 1</Menu.Item>
      <Menu.Item key="2">Notification 2</Menu.Item>
      <Menu.Item key="3">Notification 3</Menu.Item>
    </Menu>
  );

  return (
    <Dropdown overlay={menu} placement="bottomRight">
      <Badge count={5}>
        <BellOutlined style={{ fontSize: '20px', marginRight: '16px' }} />
      </Badge>
    </Dropdown>
  );
};

export default NotificationIcon;
