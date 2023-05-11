import React, { useState } from 'react'
import { useEffect } from "react";
import {GetAllUsers} from './API'
import { CanvasJSChart } from 'canvasjs-react-charts';

function Graph() {
  const [chartData, setChartData] = useState([
 
  ]);

  const fetchData = async () => {
    
   let data=[]
  
      data= await GetAllUsers();
   
      data.sort((a, b) => new Date(a.Date) - new Date(b.Date));
    setChartData(data);
  };
  
  useEffect(() => {
    fetchData()
  }, []);
  
  const options = {
    animationEnabled: true,
    title: {
      text: "Number of Active Patients"
    },
    axisY : {
      title: "Number of Patients"
    },
    toolTip: {
      shared: true
    },
    data: [{
      type: "spline",
      name: "Active Patients",
      showInLegend: true,
      dataPoints: chartData.map(({Date, ActivePatients}) => ({y: ActivePatients, label: Date}))
    }]
  };

  return (
    <div>
      <CanvasJSChart options={options} />
    </div>
  );
}

export default Graph;


