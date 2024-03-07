const observer = new IntersectionObserver(
  (entries) => {
    entries.forEach((entry) => {
      if (entry.isIntersecting) {
        entry.target.classList.add("show");
      } else {
        entry.target.classList.remove("show");
      }
    });
  },
  { threshold: 0.2 }
);

// server = `https://proj.ruppin.ac.il/cgroup54/test2/tar1/`;
server =
  location.hostname === "localhost" || location.hostname === "127.0.0.1"
    ? `https://localhost:7014/`
    : `https://proj.ruppin.ac.il/cgroup54/test2/tar1/`;