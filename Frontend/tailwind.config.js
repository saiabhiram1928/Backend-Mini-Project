/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["*.{html,js}"],
  theme: {
    // extend: {
    //   keyframes: {
    //     rubberband: {
    //       '0%': { transform: 'scale(1)' },
    //       '30%': { transform: 'scale(1.25, 0.75)' },
    //       '40%': { transform: 'scale(0.75, 1.25)' },
    //       '50%': { transform: 'scale(1.15, 0.85)' },
    //       '65%': { transform: 'scale(0.95, 1.05)' },
    //       '75%': { transform: 'scale(1.05, 0.95)' },
    //       '100%': { transform: 'scale(1)' },
    //     },
    //   },
    //   animation: {
    //     rubberband: 'rubberband 1s ease',
    //   },
    //   colors: {
    //     gold: '#FFD700',
    //   },
    // },
  },
  plugins: [
    require('daisyui'),
  ],
}