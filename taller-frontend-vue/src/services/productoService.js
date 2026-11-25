import axios from 'axios';

// Apunta al puerto donde corre tu TallerApi en .NET
const API_URL = 'https://potential-engine-6w9x779vg76359rp-5000.app.github.dev/api/Productos';

export default {
  async obtenerProductos() {
    try {
      const response = await axios.get(API_URL);
      return response.data;
    } catch (error) {
      console.error('Error al conectar con la API de productos:', error);
      throw error;
    }
  }
};
