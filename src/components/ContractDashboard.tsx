import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Pie } from '@ant-design/charts';
import { Card, Typography, Spin } from 'antd';

const { Title } = Typography;

interface ContractReport {
  ContractTypeId: number;
  ContractTypeName: string;
  EmployeeCount: number;
}

const ContractDashboard: React.FC = () => {
  const [data, setData] = useState<ContractReport[] | null>(null);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const testData: ContractReport[] = [
          {
            ContractTypeId: 1,
            ContractTypeName: "Hợp đồng đào tạo",
            EmployeeCount: 10
          },
          {
            ContractTypeId: 2,
            ContractTypeName: "Hợp đồng học viên",
            EmployeeCount: 20
          },
          {
            ContractTypeId: 3,
            ContractTypeName: "Hợp đồng thử việc",
            EmployeeCount: 30
          },
          {
            ContractTypeId: 4,
            ContractTypeName: "Hợp đồng chính thức",
            EmployeeCount: 40
          },
          {
            ContractTypeId: 5,
            ContractTypeName: "Hợp đồng cộng tác viên",
            EmployeeCount: 50
          }
        ];
        setData(testData);

        
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
    angleField: 'EmployeeCount',
    colorField: 'ContractTypeName',
    radius: 0.8,
    legend: {
      position: 'top-left',
    },
    label: {
      type: 'outer',
      content: '{name}: {percentage}',
    },
    interactions: [{ type: 'element-active' }],
  };

  return (
    <Card>
      <Title level={2}>Contract Type Distribution</Title>
      {loading ? (
        <Spin size="large" />
      ) : (
          <Pie {...config} />
      )}
    </Card>
  );
};

export default ContractDashboard;
