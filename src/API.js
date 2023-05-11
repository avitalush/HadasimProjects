import axios from 'axios';
export const GetAllUsers = async () => {
    debugger
    return await (await axios.get(`https://localhost:44355/api/graphsData`)).data;
}