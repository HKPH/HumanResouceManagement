import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Pie } from '@ant-design/charts';
import { Card, Typography, Spin } from 'antd';

const { Title } = Typography;
const API_URL = 'https://localhost:7005/api/Report/active-contracts';

interface ContractReport {
  contractId: number;
  contractName: string;
  employeeCount: number;
}

const ContractDashboard: React.FC = () => {
  const [data, setData] = useState<ContractReport[] | null>(null);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(API_URL);
        console.log("data", response.data);
        setData(response.data);
      } catch (error) {
        console.error('Error fetching contract data:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  const config = {
    data: data || [],
    angleField: 'employeeCount',
    colorField: 'contractName',
    radius: 0.8,
    legend: {
      position: 'top-left',
    },
    label: {
      content: '{name}: {percentage}',
    },
    interactions: [{ type: 'element-active' }],
  };

  return (
    <Card>
      <Title level={2}>Hợp đồng lao động</Title>
      {loading ? (
        <Spin size="large" />
      ) : (
        <Pie {...config} />
      )}
    </Card>
  );
};

export default ContractDashboard;
