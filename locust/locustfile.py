from locust import HttpUser, task, events
import gevent
import random
import time

# ==============================
# CONFIGURACIÓN DE CARGA
# ==============================
REQUESTS_PER_WINDOW = 5
WINDOW_SECONDS = 10


class MyApiUser(HttpUser):
    wait_time = lambda self: 0

    @task
    def post_gethash(self):
        # URL COMPLETA (normalizada, igual que Postman)
        url = (
            "/?xpvfx=gethash"
            "&p1=192.168.112.174:81"
            "&p2=cualquier cosa"
            "&p3=123456789012345"
            "&p4=cualquier cosa"
            "&p5=_00"
        )

        response = self.client.post(
            url,
            data="",        # POST sin body (Content-Length: 0)
            verify=False    # certificado interno
        )

        print("STATUS:", response.status_code)
        print("RESPONSE:", response.text[:2000])


# ==============================
# SCHEDULER POR VENTANAS
# ==============================
def schedule_requests(environment):
    user_class = environment.runner.user_classes[0]

    while True:
        window_start = time.time()

        for _ in range(REQUESTS_PER_WINDOW):
            delay = random.uniform(0, WINDOW_SECONDS)
            gevent.spawn_later(
                delay,
                user_class(environment).post_gethash
            )

        elapsed = time.time() - window_start
        gevent.sleep(max(0, WINDOW_SECONDS - elapsed))


# ==============================
# HOOK DE INICIO
# ==============================
@events.test_start.add_listener
def on_test_start(environment, **kwargs):
    gevent.spawn(schedule_requests, environment)
