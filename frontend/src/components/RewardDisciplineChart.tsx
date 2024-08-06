import React, { useState } from 'react';
import { Line } from '@ant-design/charts';
import { DatePicker, Button, message, Typography } from 'antd';
import dayjs, { Dayjs } from 'dayjs';
import { fetchReportData } from '../api/reportApi';

const { RangePicker } = DatePicker;
const { Title } = Typography;

interface ReportData { 
  year: number;
  month: number;
  totalDisciplines: number;
  totalRewards: number;
}

interface ChartData {
  month: string;
  type: string;
  value: number;
}

const RewardDisciplineChart: React.FC = () => {
  const [chartData, setChartData] = useState<ChartData[]>([]);
  const [dates, setDates] = useState<[Dayjs, Dayjs]>([
    dayjs().subtract(1, 'year').startOf('day'),
    dayjs().endOf('day'),
  ]);
  const [requestParams, setRequestParams] = useState<{ startDate: string; endDate: string } | null>(null);
  const [responseData, setResponseData] = useState<any>(null);

  const handleFetchData = async () => {
    if (dates) {
      const startDate = dates[0];
      const endDate = dates[1];

      setRequestParams({ startDate: startDate.toISOString(), endDate: endDate.toISOString() });

      try {
        const data = await fetchReportData(startDate, endDate);
        setResponseData(data);

        const formattedData = data.reports.map((item: ReportData) => [
          { month: `${item.year}-${item.month.toString().padStart(2, '0')}`, type: 'Kỷ luật', value: item.totalDisciplines },
          { month: `${item.year}-${item.month.toString().padStart(2, '0')}`, type: 'Khen thưởng', value: item.totalRewards },
        ]).flat();

        setChartData(formattedData);
      } catch (error) {
        message.error('Không thể lấy dữ liệu từ API.');
      }
    }
  };

  const config = {
    data: chartData,
    xField: 'month',
    yField: 'value',
    seriesField: 'type',
    xAxis: {
      type: 'cat',
      title: {
        text: 'Tháng',
      },
    },
    yAxis: {
      title: {
        text: 'Số lượng',
      },
    },
    lineStyle: {
      lineWidth: 2,
    },
    point: {
      size: 5,
      shape: 'circle',
    },
    legend: {
      position: 'top-left',
    },
    color: ({ type }: { type: string }) => (type === 'Kỷ luật' ? '#FF4D4F' : '#52C41A'),
  };

  return (
    <div>
      <Title level={2}>Biểu đồ Kỷ luật và Khen thưởng</Title>
      {dates && (
        <RangePicker
          format="YYYY-MM-DD"
          value={dates}
          onChange={(dates) => setDates(dates as [Dayjs, Dayjs])}
        />
      )}
      <Button
        type="primary"
        onClick={handleFetchData}
        disabled={!dates}
      >
        Lấy dữ liệu
      </Button>
      <Line {...config} />
    </div>
  );
};

export default RewardDisciplineChart;
