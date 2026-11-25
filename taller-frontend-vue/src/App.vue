<template>
  <!-- Si no hay sesión activa, el Login es siempre la pantalla principal -->
  <LoginView v-if="!sesion" @login-success="onLoginSuccess" />

  <!-- Sistema principal TallerPro -->
  <v-app v-else>
    <!-- Barra Lateral (Sidebar Profesional) -->
    <v-navigation-drawer
      v-model="drawer"
      :rail="rail && !isMobile"
      :permanent="!isMobile"
      :temporary="isMobile"
      @click="rail = false"
      color="surface"
      elevation="1"
    >
      <v-list-item nav class="py-4">
        <template #prepend>
          <v-avatar rounded="lg" size="40" class="bg-primary text-white mr-3">
            <v-icon icon="mdi-car-wrench" />
          </v-avatar>
        </template>
        <v-list-item-title class="font-weight-bold text-primary text-h6">TallerPro</v-list-item-title>
        <v-list-item-subtitle v-if="!rail">Sistema de Gestión</v-list-item-subtitle>
        <template #append v-if="!isMobile">
          <v-btn variant="text" icon="mdi-chevron-left" @click.stop="rail = !rail" />
        </template>
      </v-list-item>

      <v-divider />

      <!-- Menú de Navegación -->
      <v-list density="compact" nav>
        <v-list-subheader>General</v-list-subheader>
        <v-list-item 
          prepend-icon="mdi-view-dashboard" 
          title="Panel / Dashboard" 
          value="dashboard" 
          :active="vistaActual === 'dashboard'"
          @click="vistaActual = 'dashboard'"
          color="primary" 
          rounded="lg"
          exact 
        />

        <v-list-subheader>Módulos de Taller</v-list-subheader>
        <v-list-item 
          prepend-icon="mdi-package-variant-closed" 
          title="Productos y Repuestos" 
          value="productos" 
          :active="vistaActual === 'productos'"
          @click="vistaActual = 'productos'"
          color="primary" 
          rounded="lg"
          exact 
        />
        <v-list-item 
          prepend-icon="mdi-shape-outline" 
          title="Categorías" 
          value="categorias" 
          :active="vistaActual === 'categorias'"
          @click="vistaActual = 'categorias'"
          color="primary" 
          rounded="lg"
          exact 
        />
        <v-list-item 
          prepend-icon="mdi-account-group" 
          title="Clientes" 
          value="clientes" 
          :active="vistaActual === 'clientes'"
          @click="vistaActual = 'clientes'"
          color="primary" 
          rounded="lg"
          exact 
        />
        <v-list-item 
          prepend-icon="mdi-car-side" 
          title="Vehículos / Carros" 
          value="carros" 
          :active="vistaActual === 'carros'"
          @click="vistaActual = 'carros'"
          color="primary" 
          rounded="lg"
          exact 
        />
      </v-list>

      <v-spacer />

      <template #append>
        <div class="pa-4 text-center text-caption text-medium-emphasis">
          <span v-if="!rail">.NET Core & MySQL</span>
        </div>
      </template>
    </v-navigation-drawer>

    <!-- Barra Superior -->
    <v-app-bar elevation="0" border color="white" class="px-4">
      <v-app-bar-nav-icon v-if="isMobile" @click="drawer = !drawer" />
      <v-toolbar-title class="font-weight-bold text-primary text-subtitle-1 text-uppercase">
        {{ vistaActual }}
      </v-toolbar-title>
      
      <v-spacer />

      <div class="d-flex align-center ga-3">
        <v-chip color="success" variant="tonal" size="small" prepend-icon="mdi-check-circle">
          API Conectada
        </v-chip>

        <span class="text-body-2 font-weight-medium d-none d-sm-inline">
          {{ sesion.nombreCompleto || sesion.nombreUsuario }}
        </span>

        <v-avatar color="primary" size="36">
          <span class="text-white text-caption font-weight-bold">{{ inicialesUsuario }}</span>
        </v-avatar>

        <v-btn
          variant="text"
          color="error"
          size="small"
          prepend-icon="mdi-logout"
          class="text-none"
          @click="cerrarSesion"
        >
          Salir
        </v-btn>
      </div>
    </v-app-bar>

    <!-- Contenido Principal -->
    <v-main class="bg-grey-lighten-4">
      <v-container fluid class="pa-6">
        
        <!-- VISTA: DASHBOARD -->
        <div v-if="vistaActual === 'dashboard'" class="d-flex flex-column ga-6">
          <v-row>
            <v-col cols="12" md="3">
              <v-card class="rounded-xl pa-4" elevation="1" border>
                <div class="text-subtitle-2 text-medium-emphasis">Total Productos</div>
                <div class="text-h4 font-weight-bold text-primary mt-2">{{ productos.length }}</div>
              </v-card>
            </v-col>
            <v-col cols="12" md="3">
              <v-card class="rounded-xl pa-4" elevation="1" border>
                <div class="text-subtitle-2 text-medium-emphasis">Categorías</div>
                <div class="text-h4 font-weight-bold text-primary mt-2">{{ categorias.length }}</div>
              </v-card>
            </v-col>
            <v-col cols="12" md="3">
              <v-card class="rounded-xl pa-4" elevation="1" border>
                <div class="text-subtitle-2 text-medium-emphasis">Clientes Registrados</div>
                <div class="text-h4 font-weight-bold text-primary mt-2">12</div>
              </v-card>
            </v-col>
            <v-col cols="12" md="3">
              <v-card class="rounded-xl pa-4" elevation="1" border>
                <div class="text-subtitle-2 text-medium-emphasis">Vehículos en Taller</div>
                <div class="text-h4 font-weight-bold text-primary mt-2">5</div>
              </v-card>
            </v-col>
          </v-row>

          <v-card class="rounded-xl pa-6" elevation="1" border>
            <h3 class="text-h6 font-weight-bold mb-2">Bienvenido al Panel de Control de TallerPro</h3>
            <p class="text-body-2 text-medium-emphasis">
              Utiliza el menú lateral para administrar inventarios, categorías de servicios, clientes y seguimiento de vehículos en tiempo real.
            </p>
          </v-card>
        </div>

        <!-- VISTA: PRODUCTOS -->
        <div v-if="vistaActual === 'productos'" class="d-flex flex-column ga-6">
          <div class="d-flex flex-wrap justify-space-between align-center ga-4">
            <v-text-field
              v-model="busqueda"
              density="comfortable"
              variant="outlined"
              placeholder="Buscar producto o lubricante..."
              prepend-inner-icon="mdi-magnify"
              hide-details
              style="max-width: 350px;"
              class="bg-white rounded-lg"
            />
            <v-btn color="primary" prepend-icon="mdi-plus" class="text-none font-weight-bold px-4" @click="mostrarModalProducto = true">
              Nuevo Producto
            </v-btn>
          </div>

          <v-row>
            <v-col cols="12" sm="6" md="4" v-for="producto in productosFiltrados" :key="producto.id_productos || producto.id">
              <v-card class="rounded-xl pa-4 d-flex flex-column justify-space-between h-100" elevation="1" border>
                <div>
                  <div class="d-flex justify-space-between align-start mb-2">
                    <h3 class="text-subtitle-1 font-weight-bold text-grey-darken-4">{{ producto.nombre }}</h3>
                    <v-chip size="x-small" color="success" variant="tonal">Activo</v-chip>
                  </div>
                  <p class="text-body-2 text-medium-emphasis mb-4">{{ producto.descripcion || 'Sin descripción detallada.' }}</p>
                </div>
                <div class="d-flex justify-space-between align-center pt-3 border-top">
                  <span class="text-h6 font-weight-bold text-success">Q{{ producto.precio }}</span>
                  <span class="text-caption text-medium-emphasis">En Stock</span>
                </div>
              </v-card>
            </v-col>
          </v-row>
        </div>

        <!-- VISTA: CATEGORÍAS -->
        <div v-if="vistaActual === 'categorias'" class="d-flex flex-column ga-6">
          <div class="d-flex flex-wrap justify-space-between align-center ga-4">
            <div>
              <h3 class="text-h6 font-weight-bold">Gestión de Categorías</h3>
              <p class="text-body-2 text-medium-emphasis">Administra las clasificaciones para los repuestos y servicios del taller.</p>
            </div>
            <v-btn color="primary" prepend-icon="mdi-plus" class="text-none font-weight-bold px-4" @click="mostrarModalCategoria = true">
              Nueva Categoría
            </v-btn>
          </div>

          <v-row>
            <v-col cols="12" sm="6" md="4" v-for="cat in categorias" :key="cat.id_categoria">
              <v-card class="rounded-xl pa-4 d-flex flex-column justify-space-between" elevation="1" border>
                <div>
                  <div class="d-flex align-center ga-3 mb-2">
                    <v-avatar color="primary-lighten-4" size="36" class="text-primary font-weight-bold">
                      <v-icon icon="mdi-shape" />
                    </v-avatar>
                    <div>
                      <h3 class="text-subtitle-1 font-weight-bold text-grey-darken-4">{{ cat.nombre }}</h3>
                      <span class="text-caption text-medium-emphasis">ID: {{ cat.id_categoria }}</span>
                    </div>
                  </div>
                  <p class="text-body-2 text-medium-emphasis mt-2">{{ cat.descripcion || 'Sin descripción asignada.' }}</p>
                </div>
              </v-card>
            </v-col>
          </v-row>
        </div>

        <!-- VISTA: CLIENTES -->
        <div v-if="vistaActual === 'clientes'" class="d-flex flex-column ga-6">
          <v-card class="rounded-xl pa-6 d-flex justify-space-between align-center" elevation="1" border>
            <div>
              <h3 class="text-h6 font-weight-bold">Gestión de Clientes</h3>
              <p class="text-body-2 text-medium-emphasis">Administra los datos de los propietarios de vehículos.</p>
            </div>
            <v-btn color="primary" variant="flat" class="text-none" @click="mostrarAlertaDesarrollo">Agregar Cliente</v-btn>
          </v-card>
          <v-card class="rounded-xl pa-12 text-center text-medium-emphasis" elevation="1" border>
            No hay clientes registrados en este módulo todavía.
          </v-card>
        </div>

        <!-- VISTA: CARROS -->
        <div v-if="vistaActual === 'carros'" class="d-flex flex-column ga-6">
          <v-card class="rounded-xl pa-6 d-flex justify-space-between align-center" elevation="1" border>
            <div>
              <h3 class="text-h6 font-weight-bold">Control de Vehículos</h3>
              <p class="text-body-2 text-medium-emphasis">Registro de automóviles ingresados al taller.</p>
            </div>
            <v-btn color="primary" variant="flat" class="text-none" @click="mostrarAlertaDesarrollo">Ingresar Vehículo</v-btn>
          </v-card>
          <v-card class="rounded-xl pa-12 text-center text-medium-emphasis" elevation="1" border>
            No hay vehículos ingresados actualmente.
          </v-card>
        </div>

      </v-container>
    </v-main>

    <!-- Modal para Nuevo Producto -->
    <v-dialog v-model="mostrarModalProducto" max-width="480">
      <v-card class="rounded-xl pa-2">
        <v-card-title class="text-subtitle-1 font-weight-bold">Registrar Nuevo Producto</v-card-title>
        <v-divider />
        <v-card-text class="pa-4">
          <v-form @submit.prevent="guardarProducto" class="d-flex flex-column ga-3">
            <v-text-field
              v-model="nuevoProducto.nombre"
              label="Nombre del Producto"
              variant="outlined"
              density="comfortable"
              required
            />
            <v-textarea
              v-model="nuevoProducto.descripcion"
              label="Descripción"
              variant="outlined"
              density="comfortable"
              rows="3"
            />
            <v-text-field
              v-model.number="nuevoProducto.precio"
              label="Precio (Q)"
              type="number"
              step="0.01"
              variant="outlined"
              density="comfortable"
              required
            />
            <v-select
              v-model="nuevoProducto.idCategoria"
              :items="categorias"
              item-title="nombre"
              item-value="idCategoria"
              label="Categoría del Servicio / Repuesto"
              variant="outlined"
              density="comfortable"
              required
            />
          </v-form>
        </v-card-text>
        <v-card-actions class="pa-4 pt-0">
          <v-spacer />
          <v-btn variant="text" class="text-none" @click="mostrarModalProducto = false">Cancelar</v-btn>
          <v-btn color="primary" variant="flat" class="text-none" @click="guardarProducto">Guardar Producto</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Modal para Nueva Categoría -->
    <v-dialog v-model="mostrarModalCategoria" max-width="480">
      <v-card class="rounded-xl pa-2">
        <v-card-title class="text-subtitle-1 font-weight-bold">Registrar Nueva Categoría</v-card-title>
        <v-divider />
        <v-card-text class="pa-4">
          <v-form @submit.prevent="guardarCategoria" class="d-flex flex-column ga-3">
            <v-text-field
              v-model="nuevaCategoria.nombre"
              label="Nombre de la Categoría"
              variant="outlined"
              density="comfortable"
              required
            />
            <v-textarea
              v-model="nuevaCategoria.descripcion"
              label="Descripción"
              variant="outlined"
              density="comfortable"
              rows="3"
            />
          </v-form>
        </v-card-text>
        <v-card-actions class="pa-4 pt-0">
          <v-spacer />
          <v-btn variant="text" class="text-none" @click="mostrarModalCategoria = false">Cancelar</v-btn>
          <v-btn color="primary" variant="flat" class="text-none" @click="guardarCategoria">Guardar Categoría</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

  </v-app>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue';
import { useDisplay } from 'vuetify';
import axios from 'axios';
import LoginView from './LoginView.vue';
import logo from './imagenes/logo.png';

const { mobile: isMobile } = useDisplay();
const sesion = ref(null);
const vistaActual = ref('dashboard');
const drawer = ref(true);
const rail = ref(false);

const productos = ref([]);
const categorias = ref([]);
const busqueda = ref('');

const mostrarModalProducto = ref(false);
const mostrarModalCategoria = ref(false);

const nuevoProducto = ref({
  nombre: '',
  descripcion: '',
  precio: null,
  idCategoria: null
});

const nuevaCategoria = ref({
  nombre: '',
  descripcion: ''
});

const API_URL = 'http://localhost:5000/api/Productos';
const API_CATEGORIAS = 'http://localhost:5000/api/CategoriasServicios';

const inicialesUsuario = computed(() => {
  const texto = sesion.value?.nombreCompleto || sesion.value?.nombreUsuario || 'TP';
  return texto
    .split(' ')
    .filter(Boolean)
    .slice(0, 2)
    .map(p => p[0].toUpperCase())
    .join('');
});

const mostrarAlertaDesarrollo = () => {
  window.alert('Módulo en desarrollo');
};

const cargarDatosTaller = async () => {
  await Promise.all([obtenerProductos(), obtenerCategorias()]);
};

const onLoginSuccess = async (nuevaSesion) => {
  sesion.value = nuevaSesion;
  vistaActual.value = 'dashboard';
  await cargarDatosTaller();
};

const cerrarSesion = () => {
  localStorage.removeItem('taller_auth');
  sessionStorage.removeItem('taller_auth');
  sesion.value = null;
};

const obtenerProductos = async () => {
  try {
    const response = await axios.get(API_URL);
    productos.value = response.data;
  } catch (error) {
    console.error('Error al cargar productos:', error);
  }
};

const obtenerCategorias = async () => {
  try {
    const response = await axios.get(API_CATEGORIAS);
    categorias.value = response.data;
  } catch (error) {
    console.error('Error al cargar categorías:', error);
  }
};

const productosFiltrados = computed(() => {
  if (!busqueda.value) return productos.value;
  return productos.value.filter(p => 
    p.nombre.toLowerCase().includes(busqueda.value.toLowerCase()) ||
    (p.descripcion && p.descripcion.toLowerCase().includes(busqueda.value.toLowerCase()))
  );
});

const guardarProducto = async () => {
  try {
    const payload = {
      nombre: nuevoProducto.value.nombre,
      descripcion: nuevoProducto.value.descripcion,
      precio: Number(nuevoProducto.value.precio),
      idCategoria: Number(nuevoProducto.value.idCategoria)
    };

    await axios.post(API_URL, payload);
    mostrarModalProducto.value = false;
    nuevoProducto.value = { nombre: '', descripcion: '', precio: null, idCategoria: null };
    await obtenerProductos();
  } catch (error) {
    console.error('Error al guardar el producto:', error.response?.data || error);
    window.alert('No se pudo guardar el producto. Verifica los campos.');
  }
};

const guardarCategoria = async () => {
  try {
    await axios.post(API_CATEGORIAS, nuevaCategoria.value);
    mostrarModalCategoria.value = false;
    nuevaCategoria.value = { nombre: '', descripcion: '' };
    await obtenerCategorias();
  } catch (error) {
    console.error('Error al guardar la categoría:', error);
    window.alert('No se pudo guardar la categoría en la base de datos.');
  }
};

onMounted(async () => {
  const raw = localStorage.getItem('taller_auth') || sessionStorage.getItem('taller_auth');
  if (raw) {
    try {
      sesion.value = JSON.parse(raw);
      await cargarDatosTaller();
    } catch {
      sesion.value = null;
    }
  }
});
</script>

<style>
body {
  font-family: 'Roboto', sans-serif;
}
</style>