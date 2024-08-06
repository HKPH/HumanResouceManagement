import React, { useState, useEffect } from 'react';
import { List, Popover, Button, Badge } from 'antd';
import { BellOutlined } from '@ant-design/icons';
import { fetchBirthdayEmployees } from '../api/notiApi';

const BirthdayNotifications: React.FC = () => {
  const [birthdayEmployees, setBirthdayEmployees] = useState<any[]>([]);
  const [visible, setVisible] = useState(false);

  useEffect(() => {
    const getBirthdayEmployees = async () => {
      const data = await fetchBirthdayEmployees();
      setBirthdayEmployees(data);
    };

    getBirthdayEmployees();
  }, []);

  const content = (
    <List
      dataSource={birthdayEmployees}
      renderItem={(item: any) => (
        <List.Item key={item.id}>
          {item.name} - {new Date(item.dob).toLocaleDateString()}
        </List.Item>
      )}
    />
  );

  const hasNotifications = birthdayEmployees.length > 0;

  return (
    <Popover
      content={content}
      title="Birthday Notifications"
      trigger="click"
      visible={visible}
      onVisibleChange={(visible) => setVisible(visible)}
    >
      <Button
        type="text"
        icon={
          <Badge count={hasNotifications ? birthdayEmployees.length : 0} showZero>
            <BellOutlined style={{ fontSize: '20px' }} />
          </Badge>
        }
        style={{
          position: 'absolute',
          right: 64,
          top: 0,
          margin: '16px',
        }}
      />
    </Popover>
  );
};

export default BirthdayNotifications;
