import os
import sys
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
import csv
import time

def setup_csv(file_name='scraperDatos.csv'):
    return os.path.join(os.path.dirname(os.path.abspath(__file__)), file_name)

def init_webdriver():
    driver = webdriver.Chrome()
    driver.maximize_window()
    driver.get('https://www.renfe.com/es')
    driver.execute_script("document.body.style.zoom='30%'")
    return driver

def accept_cookies(driver):
    cookies_button = WebDriverWait(driver, 10).until(
        EC.element_to_be_clickable((By.ID, 'onetrust-accept-btn-handler'))
    )
    cookies_button.click()

def configure_route(driver, origin, destination):
    origin_input = WebDriverWait(driver, 10).until(
        EC.element_to_be_clickable((By.ID, 'origin'))
    )
    origin_input.send_keys(origin)
    WebDriverWait(driver, 10).until(
        EC.element_to_be_clickable((By.XPATH, "//ul[@role='listbox']/li[1]"))
    ).click()

    destination_input = WebDriverWait(driver, 10).until(
        EC.element_to_be_clickable((By.ID, 'destination'))
    )
    destination_input.send_keys(destination)
    destination_input.send_keys(Keys.ARROW_DOWN)
    destination_input.send_keys(Keys.ENTER)

def configure_dates(driver, departure_date, return_date):
    driver.execute_script("document.body.style.zoom='50%'")
    departure_date_input = WebDriverWait(driver, 10).until(
        EC.element_to_be_clickable((By.ID, 'first-input'))
    )
    departure_date_input.click()
    WebDriverWait(driver, 10).until(
        EC.element_to_be_clickable(
            (By.XPATH, f"//section[contains(@class, 'lightpick__month')]//div[contains(@class, 'lightpick__day') and text()='{departure_date}']")
        )
    ).click()

    return_date_input = WebDriverWait(driver, 10).until(
        EC.element_to_be_clickable((By.ID, 'second-input'))
    )
    return_date_input.click()
    WebDriverWait(driver, 10).until(
        EC.element_to_be_clickable(
            (By.XPATH, f"//section[contains(@class, 'lightpick__month')]//div[contains(@class, 'lightpick__day') and text()='{return_date}']")
        )
    ).click()

def configure_passengers(driver, num_adults, num_children, num_infants):
    passengers_input = WebDriverWait(driver, 10).until(
        EC.element_to_be_clickable((By.ID, 'passengersSelection'))
    )
    passengers_input.click()

    for _ in range(num_adults - 1):
        WebDriverWait(driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "//button[contains(@aria-label, 'añadir pasajero adulto')]"))
        ).click()
    for _ in range(num_children):
        WebDriverWait(driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "//button[contains(@aria-label, 'añadir pasajero niño')]"))
        ).click()
    for _ in range(num_infants):
        WebDriverWait(driver, 10).until(
            EC.element_to_be_clickable((By.XPATH, "//button[contains(@aria-label, 'añadir pasajero niño menor de 4 años')]"))
        ).click()

def start_search(driver):
    search_button = WebDriverWait(driver, 10).until(
        EC.element_to_be_clickable((By.ID, 'ticketSearchBt'))
    )
    search_button.click()
    WebDriverWait(driver, 10).until(EC.presence_of_element_located((By.CLASS_NAME, 'selectedTren')))

def process_results(driver, csv_path):
    def save_results(results, writer, tipo_viaje):
        for result in results:
            salida = result.find_element(By.XPATH, ".//h5[1]").text.strip()
            llegada = result.find_element(By.XPATH, ".//h5[2]").text.strip()
            duracion = result.find_element(By.CLASS_NAME, 'entre-horas').text.strip()
            tipo_transbordo = 'Directo' if 'Transbordo' not in result.text else 'Con Transbordo'
            tarifas = result.find_elements(By.CLASS_NAME, 'estilo-box-card')
            if salida and llegada and duracion:
                for tarifa in tarifas:
                    titulo_tarifa = tarifa.get_attribute('data-titulo-tarifa').strip()
                    precio_tarifa = tarifa.get_attribute('data-precio-tarifa').strip()
                    if titulo_tarifa and precio_tarifa:
                        writer.writerow([salida, llegada, duracion, tipo_transbordo, titulo_tarifa, precio_tarifa, tipo_viaje])

    with open(csv_path, mode='w', newline='', encoding='utf-8') as file:
        writer = csv.writer(file)
        writer.writerow(['Salida', 'Llegada', 'Duración', 'Tipo Transbordo', 'Tarifa', 'Precio', 'IdaVuelta'])

        # Guardar resultados de la ida
        results_ida = driver.find_elements(By.CLASS_NAME, 'selectedTren')
        if results_ida:
            save_results(results_ida, writer, "Ida")

        # Cambiar a la pestaña de vuelta y guardar resultados
        vuelta_button = WebDriverWait(driver, 10).until(
            EC.element_to_be_clickable((By.ID, 'stv-tab2'))
        )
        vuelta_button.click()
        WebDriverWait(driver, 10).until(EC.presence_of_element_located((By.CLASS_NAME, 'selectedTren')))
        results_vuelta = driver.find_elements(By.CLASS_NAME, 'selectedTren')
        if results_vuelta:
            save_results(results_vuelta, writer, "Vuelta")

def main():
    csv_path = setup_csv()
    origin, destination = sys.argv[1], sys.argv[2]
    departure_date, return_date = int(sys.argv[3]), int(sys.argv[4])
    num_adults, num_children, num_infants = int(sys.argv[5]), int(sys.argv[6]), int(sys.argv[7])

    driver = init_webdriver()
    accept_cookies(driver)
    configure_route(driver, origin, destination)
    configure_dates(driver, departure_date, return_date)
    configure_passengers(driver, num_adults, num_children, num_infants)
    start_search(driver)
    process_results(driver, csv_path)
    driver.quit()

if __name__ == "__main__":
    main()
