import React, { useState } from 'react';
import { Pie } from '@ant-design/charts';
import { DatePicker, Button, Typography, message } from 'antd';
import dayjs, { Dayjs } from 'dayjs';

const { RangePicker } = DatePicker;
const { Title, Paragraph } = Typography;

interface ReportData {
  newEmployeesCount: number;
  terminatedEmployeesCount: number;
  totalEmployeeCount: number;
}

const EmployeeContractReport: React.FC = () => {
  const [reportData, setReportData] = useState<ReportData | null>(null);
  const [dates, setDates] = useState<[Dayjs, Dayjs]>([
    dayjs().subtract(1, 'year').startOf('day'),
    dayjs().endOf('day'),
  ]);
  const [requestParams, setRequestParams] = useState<{ startDate: string; endDate: string } | null>(null);

  const fetchData = async (startDate: Dayjs, endDate: Dayjs) => {
    const startDateISO = startDate.toISOString();
    const endDateISO = endDate.toISOString();

    setRequestParams({ startDate: startDateISO, endDate: endDateISO });

    // Fake data for testing
    const fakeData: ReportData = {
      newEmployeesCount: 10,
      terminatedEmployeesCount: 5,
      totalEmployeeCount: 100,
    };

    setReportData(fakeData);

    // Uncomment the below lines to use the actual API
    // try {
    //   const response = await axios.get('https://localhost:7005/api/employee-contract-report', {
    //     params: {
    //       startDate: startDateISO,
    //       endDate: endDateISO,
    //     },
    //   });

    //   setReportData(response.data);
    // } catch (error) {
    //   console.error('Failed to fetch data', error);
    //   message.error('Không thể lấy dữ liệu từ API.');
    // }
  };

  const handleFetchData = () => {
    if (dates) {
      fetchData(dates[0], dates[1]);
    }
  };

  const pieData = reportData
    ? [
        { type: 'New Employees', value: reportData.newEmployeesCount },
        { type: 'Terminated Employees', value: reportData.terminatedEmployeesCount },
        { type: 'Remaining Employees', value: reportData.totalEmployeeCount - reportData.newEmployeesCount - reportData.terminatedEmployeesCount },
      ]
    : [];

  const config = {
    appendPadding: 10,
    data: pieData,
    angleField: 'value',
    colorField: 'type',
    radius: 1,
    label: {
      content: '{name} ({percentage})',
    },
    interactions: [{ type: 'element-active' }],
  };

  return (
    <div>
      <Title level={2}>Báo cáo Hợp đồng Nhân viên</Title>
      <RangePicker
        format="YYYY-MM-DD"
        value={dates}
        onChange={(dates) => setDates(dates as [Dayjs, Dayjs])}
      />
      <Button
        type="primary"
        onClick={handleFetchData}
        disabled={!dates}
      >
        Lấy dữ liệu
      </Button>
      {requestParams && (
        <Paragraph>
          <strong>JSON gửi đi:</strong>
          <pre>{JSON.stringify(requestParams, null, 2)}</pre>
        </Paragraph>
      )}
      {reportData && (
        <Paragraph>
          <strong>Dữ liệu nhận về:</strong>
          <pre>{JSON.stringify(reportData, null, 2)}</pre>
        </Paragraph>
      )}
      {reportData && pieData.length > 0 && <Pie {...config} />}
    </div>
  );
};

export default EmployeeContractReport;
