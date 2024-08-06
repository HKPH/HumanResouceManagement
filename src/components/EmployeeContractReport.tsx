import React, { useState } from 'react';
import { Pie } from '@ant-design/charts';
import { DatePicker, Button, Typography, message } from 'antd';
import dayjs, { Dayjs } from 'dayjs';
import { fetchEmployeeContractReport } from '../api/reportApi';

const { RangePicker } = DatePicker;
const { Title } = Typography;

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

    try {
      const data = await fetchEmployeeContractReport(startDate, endDate);
      setReportData(data);
    } catch (error) {
      message.error('Không thể lấy dữ liệu từ API.');
    }
  };

  const handleFetchData = () => {
    if (dates) {
      fetchData(dates[0], dates[1]);
    }
  };

  const pieData = reportData
    ? [
        { type: 'Nhân viên mới', value: reportData.newEmployeesCount },
        { type: 'Nhân viên nghỉ việc', value: reportData.terminatedEmployeesCount },
        { type: 'Nhân viên cũ', value: reportData.totalEmployeeCount - reportData.newEmployeesCount - reportData.terminatedEmployeesCount },
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
      {reportData && pieData.length > 0 && <Pie {...config} />}
    </div>
  );
};

export default EmployeeContractReport;
