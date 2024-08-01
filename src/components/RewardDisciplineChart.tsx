import React, { useState } from 'react';
import { Line } from '@ant-design/charts';
import { DatePicker, Button, message, Typography } from 'antd';
import axios from 'axios';
import dayjs, { Dayjs } from 'dayjs';

const { RangePicker } = DatePicker;
const { Title, Paragraph } = Typography;

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

  const fetchData = async (startDate: Dayjs, endDate: Dayjs) => {
    const startDateISO = startDate.toISOString();
    const endDateISO = endDate.toISOString();

    setRequestParams({ startDate: startDateISO, endDate: endDateISO });

    try {
      const response = await axios.get('https://localhost:7005/api/Report/monthly-report', {
        params: {
          startDate: startDateISO,
          endDate: endDateISO,
        },
      });

      setResponseData(response.data);

      const data = response.data.map((item: ReportData) => [
        { month: `${item.year}-${item.month.toString().padStart(2, '0')}`, type: 'Kỷ luật', value: item.totalDisciplines },
        { month: `${item.year}-${item.month.toString().padStart(2, '0')}`, type: 'Khen thưởng', value: item.totalRewards },
      ]).flat();
      setChartData(data);
    } catch (error) {
      console.error('Failed to fetch data', error);
      message.error('Không thể lấy dữ liệu từ API.');
    }
  };

  const handleFetchData = () => {
    if (dates) {
      fetchData(dates[0], dates[1]);
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
      {requestParams && (
        <Paragraph>
          <strong>JSON gửi đi:</strong>
          <pre>{JSON.stringify(requestParams, null, 2)}</pre>
        </Paragraph>
      )}
      {responseData && (
        <Paragraph>
          <strong>Dữ liệu nhận về:</strong>
          <pre>{JSON.stringify(responseData, null, 2)}</pre>
        </Paragraph>
      )}
      <Line {...config} />
    </div>
  );
};

export default RewardDisciplineChart;
