import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'
import tailwindcss from '@tailwindcss/vite'

// https://vite.dev/config/
export default defineConfig({
  server: {
    port: 3000,//j ai changer le port car j ai une autre app runing
  },
  plugins: [tailwindcss(),//pour utiliser tailwind
    react()],
})
