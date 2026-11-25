<script setup>
import { ref, reactive } from 'vue';
import axios from 'axios';
import logo from './imagenes/logo.png';

const emit = defineEmits(['login-success']);

// Ajusta el puerto si tu TallerApi corre en otro puerto
const API_AUTH_URL = 'http://localhost:5000/api/Auth/login';

const form = reactive({
  nombreUsuario: '',
  password: '',
  recordar: true,
});

const mostrarPassword = ref(false);
const cargando = ref(false);
const errorMsg = ref('');

const handleLogin = async () => {
  errorMsg.value = '';

  if (!form.nombreUsuario.trim() || !form.password) {
    errorMsg.value = 'Ingresa tu nombre de usuario y contraseña.';
    return;
  }

  cargando.value = true;

  try {
    const { data } = await axios.post(API_AUTH_URL, {
      nombreUsuario: form.nombreUsuario.trim(),
      password: form.password,
    });

    const sesion = {
      idUsuario: data.idUsuario,
      nombreUsuario: data.nombreUsuario,
      nombreCompleto: data.nombreCompleto,
      rol: data.rol,
      token: data.token,
    };

    const storage = form.recordar ? localStorage : sessionStorage;
    storage.setItem('taller_auth', JSON.stringify(sesion));

    emit('login-success', sesion);
  } catch (error) {
    errorMsg.value =
      error.response?.data?.mensaje ||
      'No se pudo iniciar sesión. Verifica tus credenciales o que la API esté activa.';
  } finally {
    cargando.value = false;
  }
};
</script>

<template>
  <v-app>
    <v-main class="login-bg d-flex align-center justify-center pa-4">
      <v-card class="rounded-xl pa-6 pa-sm-8" max-width="430" width="100%" elevation="8" border>
        <div class="text-center mb-5">
          <div class="logo-box mx-auto mb-3 pa-3 rounded-xl bg-white elevation-3">
            <img
              :src="logo"
              alt="Peña's Racing Motorsport"
              class="logo-img"
            />
          </div>
          <p class="text-body-2 text-grey-lighten-1 mt-1">
            Inicia sesión para acceder al sistema del taller
          </p>
        </div>

        <v-alert
          v-if="errorMsg"
          type="error"
          variant="tonal"
          density="compact"
          class="mb-4 rounded-lg"
        >
          {{ errorMsg }}
        </v-alert>

        <v-form @submit.prevent="handleLogin" class="d-flex flex-column ga-3">
          <v-text-field
            v-model="form.nombreUsuario"
            label="Nombre de usuario"
            prepend-inner-icon="mdi-account-outline"
            variant="outlined"
            density="comfortable"
            :disabled="cargando"
            required
          />

          <v-text-field
            v-model="form.password"
            label="Contraseña"
            prepend-inner-icon="mdi-lock-outline"
            :append-inner-icon="mostrarPassword ? 'mdi-eye-off' : 'mdi-eye'"
            @click:append-inner="mostrarPassword = !mostrarPassword"
            :type="mostrarPassword ? 'text' : 'password'"
            variant="outlined"
            density="comfortable"
            :disabled="cargando"
            required
          />

          <v-checkbox
            v-model="form.recordar"
            label="Mantener sesión iniciada"
            density="compact"
            hide-details
            color="primary"
            :disabled="cargando"
          />

          <v-btn
            type="submit"
            color="primary"
            size="large"
            block
            class="text-none font-weight-bold mt-2 rounded-lg"
            :loading="cargando"
          >
            Iniciar sesión
          </v-btn>
        </v-form>
      </v-card>
    </v-main>
  </v-app>
</template>

<style scoped>
.login-bg {
  min-height: 100vh;
  background: linear-gradient(135deg, #0f172a 0%, #1e293b 55%, #0f172a 100%);
}

.logo-box {
  max-width: 320px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.logo-img {
  width: 100%;
  max-height: 92px;
  object-fit: contain;
  display: block;
}
</style>