import { toast } from 'react-semantic-toasts';
import Axios from "axios";

export const makeAxiosInstance = () => Axios.create({
  baseURL: '/api/v1',
  responseType: 'json',
  headers: {
    'Content-Type': 'application/json'
  }
});

export const showToast = (type: 'success' | 'error', desc: string) => {
  toast({
    type: type,
    title: type === 'success' ? 'Great news!' : 'Oops!',
    description: desc,
    animation: 'bounce',
    time: 2000
  });
};
