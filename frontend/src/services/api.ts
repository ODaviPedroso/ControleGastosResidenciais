import axios from 'axios';

// Instância base do axios apontando para a API .NET
const api = axios.create({
  baseURL: 'http://localhost:5241',
});

export default api;
